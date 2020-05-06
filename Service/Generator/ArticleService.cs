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
        //public async Task<Result> AddArticle(string Url)
        //{
          
        //}

        private void GetData(string d)
        {
            var sd = d;
        }

    }
}
