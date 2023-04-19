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
            //User[] users = 
            //{
            //    new()
            //    {
            //        UserName = "Nicolas", FirstName = "Nicolas", LastName = "Vanruysseveldt",
            //        Email = "nic.vanruy@gmail.com", Role = "Admin"
            //    },
            //    new()
            //    {
            //        UserName = "Joske", FirstName = "Joske", LastName = "Peeters",
            //        Email = "Joske.Peeters@gmail.com", Role = "User"
            //    }

            //};
            //UserLogin[] logins =
            //{
            //    new()
            //    {
            //        UserName = "Nicolas",
            //        Password = "Test123"
            //    },
            //    new()
            //    {
            //        UserName = "Joske", Password = "Test123"
            //    }
            //};
            modelBuilder.Entity<User>()/*.HasData(users)*/;
            modelBuilder.Entity<UserLogin>()/*.HasData(logins)*/;
            modelBuilder.Entity<PasswordReset>();
            modelBuilder.Entity<Salt>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
