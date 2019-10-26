using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sys.Reponsitory.Core;
using Sys.Reponsitory.Domain.Model;
using System;
using System.Linq;
using System.Reflection;

namespace Sys.Reponsitory
{
    public class SysDbContext : DbContext
    {
        public SysDbContext(DbContextOptions<SysDbContext> options)
           : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.ExportedTypes)
            {
                if (type.IsClass && type != typeof(Entity) && typeof(Entity).IsAssignableFrom(type))
                {
                    var method = modelBuilder.GetType().GetMethods().Where(x => x.Name == "Entity").FirstOrDefault();

                    if (method != null)
                    {
                        method = method.MakeGenericMethod(new Type[] { type });
                        method.Invoke(modelBuilder, null);
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }

}
