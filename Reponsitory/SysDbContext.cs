using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Core;
using Model;
using System;
using System.Linq;
using System.Reflection;

namespace Reponsitory
{
    public class SysDbContext : DbContext
    {
        public SysDbContext(DbContextOptions<SysDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }

}
