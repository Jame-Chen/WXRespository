using System;
using System.Data;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zxw.Framework.NetCore.CodeGenerator;
using Zxw.Framework.NetCore.Options;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// ´úÂëÉú³É
        /// </summary>
        [TestMethod]
        public void TestCodeGenerator()
        {
            CodeGenerator cg = new CodeGenerator(new CodeGenerateOption { 
               OutputPath="D:\\Test\\Mysql",
            ModelsNamespace="Model",
            ServicesNamespace="Service",
            ControllersNamespace="MyNetCore.Controller"
            });
            cg.Generate();
        }
    }
}
