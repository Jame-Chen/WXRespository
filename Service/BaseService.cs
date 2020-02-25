using Model.Core;
using Model;
using Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
