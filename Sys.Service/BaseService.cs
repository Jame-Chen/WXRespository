using Sys.Reponsitory.Core;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Service
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

        public List<T> GetAll()
        {
            List<T> list = Reponsitory.Find().ToList();
            return list;
        }

        public T GetModelById(string Id)
        {
            T model = Reponsitory.Find(f => f.Id == Id).FirstOrDefault();
            return model;
        }

        public void AddEntity(T Entity)
        {
            Reponsitory.Add(Entity);
        }

        public void BatchAdd(T[] Entities)
        {
            Reponsitory.BatchAdd(Entities);
        }

        public void UpdateEntity(T Entity)
        {
            Reponsitory.Update(Entity);
        }

        public void DeleteEntity(string Id)
        {
            Reponsitory.Delete(d => d.Id == Id);
        }

        public void BatchDelete(string ids)
        {
            Reponsitory.Delete(d => ids.Contains(d.Id));
        }
    }
}
