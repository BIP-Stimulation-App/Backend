using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StimulationAppAPI.DAL.Model;


namespace StimulationAppAPI.DAL.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> Logins { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Salt> salts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<User>();
            modelBuilder.Entity<UserLogin>();
            modelBuilder.Entity<PasswordReset>();
            modelBuilder.Entity<Salt>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
