using EZParkin.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.SetInitializer<AppDbContext>(null);
        }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region User's configuration

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Name).IsRequired();
            builder.Entity<User>().Property(p => p.Email).IsRequired();
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.IsEmailVerified).IsRequired();
            builder.Entity<User>().Property(p => p.CreatedAt).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();

            builder.Entity<User>().HasMany(p => p.Vehicle).WithOne(p => p.User).HasForeignKey(p => p.UserId);

            #endregion

            #region Vehicle's configuration

            builder.Entity<Vehicle>().ToTable("Vehicles");
            builder.Entity<Vehicle>().HasKey(p => p.Id);
            builder.Entity<Vehicle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Vehicle>().Property(p => p.Alias).IsRequired();
            builder.Entity<Vehicle>().Property(p => p.LicensePlate).IsRequired();

            #endregion
        }
    }
}
