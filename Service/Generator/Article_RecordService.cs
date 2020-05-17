using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service
{
    public partial class Article_RecordService : BaseService<Article_Record>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Article_RecordService(IUnitWork UnitWork, IReponsitory<Article_Record> reponsitory, IHostingEnvironment hostingEnvironment) : base(UnitWork, reponsitory)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        
        public Result AddArticle(Article_Record model)
        {
            Result ret = new Result();
            Expression<Func<Article_Record, bool>> exp = f => f.is_delete == 0 && f.article_url == model.article_url && f.article_userid == model.article_userid;
            if (UnitWork.IsExist<Article_Record>(exp))
            {
                var articleid = UnitWork.FindSingle<Article_Record>(exp).Id;
                var articlestatus = UnitWork.FindSingle<Article_Status>(f => f.is_delete == 0 && f.article_id == articleid);
                articlestatus.article_read_max = articlestatus.article_read_max + 20;
                articlestatus.article_status = 0;
                articlestatus.gmt_modified = DateTime.Now;
                UnitWork.Update<Article_Status>(articlestatus);
            }
            else
            {
                UnitWork.Add<Article_Record>(model);
                Article_Status status = new Article_Status();
                status.article_id = model.Id;
                status.article_status = 0;
                status.article_read_number = 0;
                status.article_read_max = 20;
                UnitWork.Add<Article_Status>(status);
            }
            if (UnitWork.IsExist<User_Info>(f => f.wechat_code == model.article_userid))
            {
                var user = UnitWork.FindSingle<User_Info>(f => f.wechat_code == model.article_userid);
                if (user.fatiecard_nm < 1)
                {
                    ret.Code = "500";
                    ret.Msg = "发帖卡数量不足！";
                    return ret;
                }
                user.fatiecard_nm = user.fatiecard_nm - 1;
                UnitWork.Update<User_Info>(user);
            }
            else
            {
                ret.Code = "500";
                ret.Msg = "用户:" + model.article_userid + ",不存在！";
                return ret;
            }
            UnitWork.Save();
            ret.Data = model;
            return ret;
        }
        public Result GetArticle(int Page, int PageSize)
        {
            Result ret = new Result();
            try
            {
                var record = UnitWork.Find<Article_Record>(f => f.is_delete == 0);
                var status = UnitWork.Find<Article_Status>(f => f.is_delete == 0&&f.article_read_max!=0);
                var user = UnitWork.Find<User_Info>(f => f.is_delete == 0);
                var querya = from a in record
                             join b in status on a.Id equals b.article_id
                             join c in user on a.article_userid equals c.wechat_code
                             select new
                             {
                                 Id = a.Id,
                                 Url = a.article_url,
                                 PicUrl = a.article_cdnurl_self != null ? a.article_cdnurl_self : a.article_cdnurl_auto,
                                 Title = a.article_title_self != null ? a.article_title_self : a.article_title_auto,
                                 Author = c.user_nickname,
                                 PubTimeDes = ConvertDateTimeInt(b.gmt_modified),
                                 ModifyTime = b.gmt_modified,
                                 PubTime = b.gmt_modified.ToString("yyyy-MM-dd"),
                                 ReadNum = b.article_read_number + "/" + b.article_read_max
                             };
                var query = querya.OrderByDescending(o => o.ModifyTime).Skip(Page * (Page - 1)).Take(PageSize);
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

        public Result ReadArticle(string Id) {
            Result ret = new Result();
            try
            {
             
                var status = UnitWork.FindSingle<Article_Status>(f => f.article_id==Id );
                if (status.article_read_max==status.article_read_number)
                {
                    ret.Code = "500";
                    ret.Msg = "文章阅读数已满！";
                    return ret;
                }
                status.article_read_number = status.article_read_number +1;
                UnitWork.Update<Article_Status>(status);
                UnitWork.Save();
                ret.Data = status;
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }

        public async Task<Article_Record> UrlParsing(string Url) {

            var data = await new Bing.Utils.Webs.Clients.WebClient().Get(Url).IgnoreSsl().ResultAsync();
            //var links = Regex.Matches(data, @"<tr\s+class=""[^>*]""><td\s+class=""country""><img\s+src=""[^>]*""\s+alt=""Cn""\s+/></td><td>(?<Ip>[^>]*)</td><td>(?<Port>[^>]*)</td><td><a\s+href=""[^>]*"">[^>]*</a></td><td\s+class=""country"">[^>]*</td><td>(?<Http>[^>]*)</td><td\s+class=""country""><div\s+title=""[^>]*""\s+class=""bar""><div\s+class=""[^>]*""\s+style=""[^>]*""></div></div></td><td\s+class=""country""><div\s+title=""[^>]*""\s+class=""bar""><div\s+class=""[^>]*""\s+style=""[^>]*""></div></div></td><td>(?<TimeDes>[^>]*)</td><td>[^>]*</td></tr>", RegexOptions.IgnoreCase);
            var title = Regex.Matches(data, @"<meta property=""og:title"" content=""(?<title>[^>]*)"" />");
            var img = Regex.Matches(data, @"<meta property=""og:image"" content=""(?<img>[^>]*)"" />");
            var creator = Regex.Matches(data, @"<meta property=""og:creator"" content=""(?<creator>[^>]*)"" />");
            //var readnum = Regex.Matches(data, @"<span id=""readNum3"">(?<readnum>[^>]*)</span>");
            Article_Record model = new Article_Record();
            model.article_url = Url;
            model.article_title_auto = title.Count > 0 ? title[0].Groups["title"].Value : "";
            model.article_cdnurl_auto = img.Count > 0 ? img[0].Groups["img"].Value : "";
            model.article_userid = creator.Count > 0 ? (creator[0].Groups["creator"].Value) : "管理员";
            //下载图片到服务器
            model.article_cdnurl_auto = DownFile(model.article_cdnurl_auto);
            return model;
        }
        public string DownFile(string url)
        {
            //获取网站当前根目录
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            //保存图片路径
            var savePath = string.Format("/File/{0}/{1}/{2}/", DateTime.Now.Year, DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"));
            //文件名
            string filename = Guid.NewGuid().ToString("N"); // System.IO.Path.GetFileName(url);
            //扩展名
            string extension = url.Contains("wx_fmt") ? "." + url.Substring(url.LastIndexOf("wx_fmt") + 7) : System.IO.Path.GetExtension(url);
            savePath = sWebRootFolder + savePath;
            //无夹创建
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            WebClient mywebclient = new WebClient();
            //下载文件
            mywebclient.DownloadFile(url, savePath + filename + extension);
            string path = string.Format("/File/{0}/{1}/{2}/{3}", DateTime.Now.Year, DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), filename + extension);
            return path;
        }

        public Result Delete(string Id)
        {
            Result ret = new Result();
            try
            {
                var Article_Record = UnitWork.FindSingle<Article_Record>(w => w.Id == Id);
                Article_Record.is_delete = 1;
                var Article_Status = UnitWork.FindSingle<Article_Status>(w => w.article_id == Id);
                Article_Status.is_delete = 1;
                UnitWork.Save();
                ret.Data = new
                {
                    Article_Record = Article_Record,
                    Article_Status = Article_Status
                };
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
