using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bing.Utils.Webs.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace MyNetCore.Controllers
{
    //[AllowAnonymous]
    public partial class ArticleController : BaseController
    {
        private readonly ArticleService article;
        public ArticleController(ArticleService _article)
        {
            article = _article;
        }
        /// <summary>
        /// 文章发帖
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> AddArticle(string Url)
        {
            Result ret = new Result();
            try
            {
                var data = await new WebClient().Get(Url).IgnoreSsl().ResultAsync();
                //var links = Regex.Matches(data, @"<tr\s+class=""[^>*]""><td\s+class=""country""><img\s+src=""[^>]*""\s+alt=""Cn""\s+/></td><td>(?<Ip>[^>]*)</td><td>(?<Port>[^>]*)</td><td><a\s+href=""[^>]*"">[^>]*</a></td><td\s+class=""country"">[^>]*</td><td>(?<Http>[^>]*)</td><td\s+class=""country""><div\s+title=""[^>]*""\s+class=""bar""><div\s+class=""[^>]*""\s+style=""[^>]*""></div></div></td><td\s+class=""country""><div\s+title=""[^>]*""\s+class=""bar""><div\s+class=""[^>]*""\s+style=""[^>]*""></div></div></td><td>(?<TimeDes>[^>]*)</td><td>[^>]*</td></tr>", RegexOptions.IgnoreCase);
                var title = Regex.Matches(data, @"<meta property=""og:title"" content=""(?<title>[^>]*)"" />");
                var img = Regex.Matches(data, @"<meta property=""og:image"" content=""(?<img>[^>]*)"" />");
                var creator = Regex.Matches(data, @"<meta property=""og:creator"" content=""(?<creator>[^>]*)"" />");
                //var readnum = Regex.Matches(data, @"<span id=""readNum3"">(?<readnum>[^>]*)</span>");
                Article model = new Article();
                model.Url = Url;
                model.Title = title.Count > 0 ? title[0].Groups["title"].Value : "";
                model.PicUrl = img.Count > 0 ? img[0].Groups["img"].Value : "";
                model.Author = creator.Count > 0 ? (creator[0].Groups["creator"].Value) : "管理员";
                model.ReadNum = 0;
                article.AddEntity(model);
                ret.Data = model;
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="Page">默认1</param>
        /// <param name="PageSize">默认20</param>
        /// <returns></returns>
        [HttpGet]
        public Result GetArticle(int Page = 1, int PageSize = 20)
        {
            return article.GetArticle(Page, PageSize);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]Article model)
        {
            return article.AddEntity(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]Article model)
        {
            return article.UpdateEntity(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return article.DeleteEntity(id);
        }
    }
}




