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
        public DbSet<Salt> Salts { get; set; }
        public DbSet<Medication> Medications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            modelBuilder.Entity<User>() //add Medication
                .HasMany(u => u.Medications)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.Dependence)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>() //add UserLogin
                .HasOne(u => u.Login)
                .WithOne(l => l.User)
                .HasForeignKey<UserLogin>(l => l.UserName)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<UserLogin>().HasKey(ul => ul.UserName);

            modelBuilder.Entity<UserLogin>()
                .HasOne(ul => ul.Salt)//adds salt
                .WithOne(s => s.UserLogin)
                .HasForeignKey<Salt>(s => s.UserName)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<UserLogin>()
                .HasOne(ul => ul.PasswordReset)//adds optionalPasswordReset
                .WithOne(s => s.UserLogin)
                .HasForeignKey<PasswordReset>(s => s.UserName)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PasswordReset>()
                .HasKey(pr => pr.UserName);

            modelBuilder.Entity<Salt>()
                .HasKey(s => s.UserName);

            modelBuilder.Entity<Medication>().HasKey(m => m.Id); ;
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
