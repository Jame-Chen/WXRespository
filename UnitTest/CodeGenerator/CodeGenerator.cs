using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zxw.Framework.NetCore.Options;
using Model.Core;
using Model;

namespace Zxw.Framework.NetCore.CodeGenerator
{
    /// <summary>
    /// 代码生成器。
    /// <remarks>
    /// 根据指定的实体域名空间生成Repositories和Services层的基础代码文件。
    /// </remarks>
    /// </summary>
    public class CodeGenerator
    {
        private string Delimiter = "\\";//分隔符，默认为windows下的\\分隔符
        public CodeGenerateOption Option { get; set; }
        /// <summary>
        /// 静态构造函数：从IoC容器读取配置参数，如果读取失败则会抛出ArgumentNullException异常
        /// </summary>
        public CodeGenerator(CodeGenerateOption option)
        {
            Option = option;
            //Options = ServiceLocator.Resolve<IOptions<CodeGenerateOption>>().Value;
            //if (Options == null)
            //{
            //    throw new ArgumentNullException(nameof(Options));
            //}
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var flag = path.IndexOf("/bin");
            if (flag > 0)
                Delimiter = "/";//如果可以取到值，修改分割符
        }

        /// <summary>
        /// 生成指定的实体域名空间下各实体对应Repositories和Services层的基础代码文件
        /// </summary>
        /// <param name="ifExistCovered">如果目标文件存在，是否覆盖。默认为false</param>
        public void Generate(bool ifExistCovered = false)
        {
            var assembly = Assembly.Load(Option.ModelsNamespace);
            var types = assembly?.GetTypes();
            var list = types?.Where(t => typeof(Entity).IsAssignableFrom(t));
            if (list != null)
            {
                foreach (var type in list)
                {
                    GenerateSingle(type, ifExistCovered);
                }
            }
        }

        /// <summary>
        /// 生成指定的实体对应IServices和Services层的基础代码文件
        /// </summary>
        /// <typeparam name="T">实体类型（必须实现IBaseModel接口）</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="ifExistCovered">如果目标文件存在，是否覆盖。默认为false</param>
        public void GenerateSingle<T, TKey>(bool ifExistCovered = false) where T : class
        {
            GenerateSingle(typeof(T), ifExistCovered);
        }

        /// <summary>
        /// 生成指定的实体对应IServices和Services层的基础代码文件
        /// </summary>
        /// <param name="modelType">实体类型（必须实现IBaseModel接口）</param>
        /// <param name="ifExistCovered">如果目标文件存在，是否覆盖。默认为false</param>
        public void GenerateSingle(Type modelType, bool ifExistCovered = false)
        {
            var modelTypeName = modelType.Name;
            var keyTypeName = modelType.GetProperty("Id")?.PropertyType.Name;
            GenerateIRepository(modelTypeName, keyTypeName, ifExistCovered);
            GenerateRepository(modelTypeName, keyTypeName, ifExistCovered);
            GenerateIService(modelTypeName, keyTypeName, ifExistCovered);
            GenerateService(modelTypeName, keyTypeName, ifExistCovered);
            GenerateController(modelTypeName, keyTypeName, ifExistCovered);
            GenerateApiController(modelTypeName, keyTypeName, ifExistCovered);
        }

        public void GenerateMyNetCore( bool ifExistCovered = false)
        {
            var assembly = Assembly.Load(Option.ModelsNamespace);
            var types = assembly?.GetTypes(); 
            var list = types?.Where(t => t.IsAbstract==false&& typeof(Entity).IsAssignableFrom(t));
            if (list != null)
            {
                foreach (var type in list)
                {
                    var modelTypeName = type.Name;
                    var keyTypeName = type.GetProperty("Id")?.PropertyType.Name;
                    GenerateService(modelTypeName, keyTypeName, "D:\\陈静\\Git项目\\MyNetCore\\Service", ifExistCovered);
                    GenerateApiController(modelTypeName, keyTypeName, "D:\\陈静\\Git项目\\MyNetCore\\MyNetCore", ifExistCovered);
                }
            }
        }

        /// <summary>
        /// 从代码模板中读取内容
        /// </summary>
        /// <param name="templateName">模板名称，应包括文件扩展名称。比如：template.txt</param>
        /// <returns></returns>
        public string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var content = string.Empty;
            var url = currentAssembly.GetName().Name + ".CodeTemplate." + templateName;
            using (var stream = currentAssembly.GetManifestResourceStream(url))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }
        public string ReadFile(string fileFullName)
        {
            return File.ReadAllText(fileFullName);
        }

        public string ReadFile(string fileType, string fileName)
        {
            var fileFullName = Option.OutputPath + Delimiter + fileType + Delimiter + fileName;
            return ReadFile(fileFullName);
        }
        /// <summary>
        /// 生成IRepository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateIRepository(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var iRepositoryPath = Option.OutputPath + Delimiter + "IRepositories";
            if (!Directory.Exists(iRepositoryPath))
            {
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + Delimiter + "I" + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", Option.IRepositoriesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        public void GenerateIRepository<TEntity, TKey>(bool ifExistCovered = false)
            => GenerateIRepository(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);
        /// <summary>
        /// 生成Repository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateRepository(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var repositoryPath = Option.OutputPath + Delimiter + "Repositories";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", Option.IRepositoriesNamespace)
                .Replace("{RepositoriesNamespace}", Option.RepositoriesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }
        public void GenerateRepository<TEntity, TKey>(bool ifExistCovered = false)
        => GenerateRepository(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);
        /// <summary>
        /// 生成IRepository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateIService(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var iRepositoryPath = Option.OutputPath + Delimiter + "IServices";
            if (!Directory.Exists(iRepositoryPath))
            {
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + Delimiter + "I" + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("IServiceTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", Option.IRepositoriesNamespace)
                .Replace("{IServicesNamespace}", Option.IServicesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        public void GenerateIService<TEntity, TKey>(bool ifExistCovered = false)
            => GenerateIService(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);
        /// <summary>
        /// 生成Repository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateService(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var repositoryPath = Option.OutputPath + Delimiter + "Services";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("ServiceTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", Option.IRepositoriesNamespace)
                .Replace("{IServicesNamespace}", Option.IServicesNamespace)
                .Replace("{ServicesNamespace}", Option.ServicesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        public void GenerateService(string modelTypeName, string keyTypeName,string outPath, bool ifExistCovered = false)
        {
            var repositoryPath = outPath + Delimiter +"Generator";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("Service.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{RepositoriesNamespace}", Option.RepositoriesNamespace)
                .Replace("{ServicesNamespace}",Option.ServicesNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }
        public void GenerateService<TEntity, TKey>(bool ifExistCovered = false)
            => GenerateService(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);

        /// <summary>
        /// 生成Controller层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateController(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var controllerPath = Option.OutputPath + Delimiter + "Controllers";
            if (!Directory.Exists(controllerPath))
            {
                Directory.CreateDirectory(controllerPath);
            }
            var fullPath = controllerPath + Delimiter + modelTypeName + "Controller.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("ControllerTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IServicesNamespace}", Option.IServicesNamespace)
                .Replace("{ControllersNamespace}", Option.ControllersNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }
        public void GenerateController<TEntity, TKey>(bool ifExistCovered = false)
            => GenerateController(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);
        /// <summary>
        /// 生成Controller层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExistCovered"></param>
        public void GenerateApiController(string modelTypeName, string keyTypeName, bool ifExistCovered = false)
        {
            var controllerPath = Option.OutputPath + Delimiter + "Controllers";
            if (!Directory.Exists(controllerPath))
            {
                Directory.CreateDirectory(controllerPath);
            }
            var fullPath = controllerPath + Delimiter + modelTypeName + "Controller.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("ApiControllerTemplate.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{IServicesNamespace}", Option.IServicesNamespace)
                .Replace("{ControllersNamespace}", Option.ControllersNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        public void GenerateApiController(string modelTypeName, string keyTypeName, string outPath, bool ifExistCovered = false)
        {
            var controllerPath = outPath + Delimiter + "Controllers"+Delimiter+"Generator";
            if (!Directory.Exists(controllerPath))
            {
                Directory.CreateDirectory(controllerPath);
            }
            var fullPath = controllerPath + Delimiter + modelTypeName + "Controller.cs";
            if (File.Exists(fullPath) && !ifExistCovered)
                return;
            var content = ReadTemplate("ApiController.txt");
            content = content.Replace("{ModelsNamespace}", Option.ModelsNamespace)
                .Replace("{ServicesNamespace}", Option.ServicesNamespace)
                .Replace("{ControllersNamespace}", Option.ControllersNamespace)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{ModelTypeNameLower}", modelTypeName.ToLower())
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }
        public void GenerateApiController<TEntity, TKey>(bool ifExistCovered = false)
            => GenerateApiController(typeof(TEntity).Name, typeof(TKey).Name, ifExistCovered);
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}
