using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bing.Utils.Webs.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace MyNetCore.Controllers
{

    public partial class Article_RecordController : BaseController
    {
        private readonly Article_RecordService article_record;
        public Article_RecordController(Article_RecordService _article_record)
        {
            article_record = _article_record;
        }
        /// <summary>
        /// Url地址解析
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> UrlParsing(string Url) {
            Result ret = new Result();
            try
            {
                var model =await  article_record.UrlParsing(Url);
                if (string.IsNullOrEmpty( model.article_cdnurl_auto)||string.IsNullOrEmpty(model.article_title_auto))
                {
                    ret.Code = "404";
                    ret.Msg = "地址无效，请检查！";
                    return ret;
                }
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
        /// 文章发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddArticle(Article_Record model)
        {
            Result ret = new Result();
            try
            {
                ret = article_record.AddArticle(model);

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
            return article_record.GetArticle(Page, PageSize);
        }
        /// <summary>
        /// 阅读文章
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost]
        public Result ReadArticle(string Id,string UserId) {
            return article_record.ReadArticle(Id,UserId);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Add([FromBody]Article_Record model)
        {
            return article_record.AddEntity(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Update([FromBody]Article_Record model)
        {
            return article_record.UpdateEntity(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public Result Delete(string id)
        {
            return article_record.Delete(id);
        }
    }
}




