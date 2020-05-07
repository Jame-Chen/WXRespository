using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Core;
using Model;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Reponsitory
{
    public class SysDbContext : DbContext
    {
        public SysDbContext(DbContextOptions<SysDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Article_Record> Article_Record { get; set; }
        public DbSet<Article_Status> Article_Status { get; set; }
        public DbSet<User_Info> User_Info { get; set; }
        public DbSet<Fatiecard_Detail> Fatiecard_Detail { get; set; }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlServer("Server=101.132.100.25;Database=WXDB;user id=sa;password=1qaz@wsx;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }

}
