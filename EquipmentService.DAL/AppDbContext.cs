using EquipmentService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentDepartment> EquipmentDepartments { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(LoggerFactory.Create(options => options.AddConsole()));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(100)")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(100)")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasColumnType("nvarchar(100)")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderType)
                    .HasColumnType("nvarchar(100)")
                    .HasMaxLength(100);
            });

        }
    }
}
