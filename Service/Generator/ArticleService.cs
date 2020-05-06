using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using Bing.Utils;
using Bing.Utils.Webs.Clients;
using System.Threading.Tasks;

namespace Service
{
    public partial class ArticleService : BaseService<Article>
    {
        public ArticleService(IUnitWork UnitWork, IReponsitory<Article> reponsitory) : base(UnitWork, reponsitory)
        {

        }

        public Result GetArticle(int Page, int PageSize)
        {
            Result ret = new Result();
            try
            {
                var query = Reponsitory.Find(Page, PageSize, s => s.InsertTime, s => true, false);
                ret.Data = query;
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }
    }
}
