﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model.Core;
using Reponsitory.Interface;
using Reponsitory;
using Z.EntityFramework.Plus;

namespace Reponsitory
{
    public class UnitWork : IUnitWork
    {
        private SysDbContext _context;

        public UnitWork(SysDbContext context)
        {
            _context = context;
        }
        public SysDbContext GetDbContext()
        {
            return _context;
        }

        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return Filter(exp);
        }

        public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var a = _context.Set<T>();
            var flag = a.Any(exp);
            return flag;
        }

        /// <summary>
        /// 查找单个
        /// </summary>
        public T FindSingle<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="orderby">排序</param>
        public IQueryable<T> Find<T, S>(int pageindex, int pagesize, Expression<Func<T, S>> orderByLambda, Expression<Func<T, bool>> exp = null, bool Asc = true) where T : class
        {
            if (pageindex < 1) pageindex = 1;
            var query = Filter(exp);
            if (Asc)
            {
                query = query.OrderBy(orderByLambda);
            }
            else
            {
                query = query.OrderByDescending(orderByLambda);
            }
            return query.Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }


        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return Filter(exp).Count();
        }

        public void Add<T>(T entity) where T : Entity
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd<T>(T[] entities) where T : Entity
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().AddRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            var entry = this._context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!this._context.ChangeTracker.HasChanges())
            {
                entry.State = EntityState.Unchanged;
            }

        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class
        {
            _context.Set<T>().Where(where).Update(entity);
        }

        public virtual void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        {
            _context.Set<T>().RemoveRange(Filter(exp));
        }

        public void Save()
        {
            _context.SaveChanges();

        }

        private IQueryable<T> Filter<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        public void ExecuteSql(string sql)
        {
            _context.Database.ExecuteSqlCommand(sql);
        }


    }
}
