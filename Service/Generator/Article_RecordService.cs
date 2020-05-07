using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace Service
{
    public partial class Article_RecordService : BaseService<Article_Record>
    {
        public Article_RecordService(IUnitWork UnitWork, IReponsitory<Article_Record> reponsitory) : base(UnitWork, reponsitory)
        {

        }
        public Result AddArticle(Article_Record model)
        {
            Result ret = new Result();
            Expression<Func<Article_Record, bool>> exp = f => f.is_delete == false && f.article_url == model.article_url && f.article_userid == model.article_userid;
            if (UnitWork.IsExist<Article_Record>(exp))
            {
                var articleid = UnitWork.FindSingle<Article_Record>(exp).Id;
                var articlestatus = UnitWork.FindSingle<Article_Status>(f => f.is_delete == false && f.article_id == articleid);
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
            return ret;
        }
        public Result GetArticle(int Page, int PageSize)
        {
            Result ret = new Result();
            try
            {
                var record = UnitWork.Find<Article_Record>(f => f.is_delete == false);
                var status = UnitWork.Find<Article_Status>(f => f.is_delete == false);
                var querya = from a in record
                             join b in status on a.Id equals b.article_id
                             select new
                             {
                                 Url = a.article_url,
                                 PicUrl = a.article_cdnurl_self != null ? a.article_cdnurl_self : a.article_cdnurl_auto,
                                 Title = a.article_title_self != null ? a.article_title_self : a.article_title_auto,
                                 Author = a.article_userid,
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

    }
}
