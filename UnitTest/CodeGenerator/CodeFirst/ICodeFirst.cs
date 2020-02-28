using System;

namespace Zxw.Framework.NetCore.CodeGenerator.CodeFirst
{
    public interface ICodeFirst
    {
        void GenerateAll(bool ifExistCovered = false);
        ICodeFirst GenerateSingle<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateIRepository<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateRepository<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateIService<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateService<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateController<T, TKey>(bool ifExistCovered = false) where T : class;
        ICodeFirst GenerateApiController<T, TKey>(bool ifExistCovered = false) where T : class;
    }


}