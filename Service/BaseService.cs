using Model.Core;
using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace Service
{
    public class BaseService<T> where T : Entity
    {
        protected IUnitWork UnitWork;
        protected IReponsitory<T> Reponsitory;

        public BaseService(IUnitWork unitWork, IReponsitory<T> repository)
        {
            UnitWork = unitWork;
            Reponsitory = repository;
        }

        public Result AddEntity(T Entity)
        {
            Result ret = new Result();
            try
            {
                Reponsitory.Add(Entity);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }
        public Result UpdateEntity(T Entity)
        {
            Result ret = new Result();
            try
            {
                Reponsitory.Update(Entity);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }

        public Result DeleteEntity(string Id)
        {
            Result ret = new Result();
            try
            {
                Reponsitory.Delete(w => w.Id == Id);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }

        public Result BatchDelete(string ids)
        {
            Result ret = new Result();
            try
            {
                Reponsitory.Delete(w => ids.Contains(w.Id));
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
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static string ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long unixTime = (long)System.Math.Round((time - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            return unixTime.ToString();
        }

        public static string Send(string httpMethod, string Url, string data)
        {
            HttpClient client = new HttpClient();
            //Uri uri = new Uri("http://localhost:59440/");
            //client.BaseAddress = uri;
            HttpResponseMessage responseMessage = null;
            httpMethod = httpMethod.ToLower();
            switch (httpMethod)
            {
                case "get":
                    responseMessage = client.GetAsync(Url).Result;
                    break;
                case "post":
                    HttpContent content = new StringContent(data);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    responseMessage = client.PostAsync(Url, content).Result;
                    break;
                case "delete":
                    //controllerName= 'api/baoxiuapi/3'
                    responseMessage = client.DeleteAsync(Url).Result;
                    break;
                case "put":
                    HttpContent content1 = new StringContent(data);
                    content1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    responseMessage = client.PutAsync(Url, content1).Result;
                    break;
                default:
                    break;


            }

            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                return result;
            }
            else
            {
                string result = "操作失败";
                return result;
            }
        }
    }
}
