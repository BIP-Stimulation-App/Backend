﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StimulationAppAPI.DAL.Context;

#nullable disable

namespace StimulationAppAPI.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230531193125_fixDateTime")]
    partial class fixDateTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<short>("Category")
                        .HasColumnType("smallint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<short>("Difficulty")
                        .HasColumnType("smallint");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Reward")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Dependence")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("UserName");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<short>("Frequency")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("Dependence");

                    b.ToTable("Medication");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.PasswordReset", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.Salt", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(30)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("VARBINARY(MAX)");

                    b.HasKey("UserName");

                    b.ToTable("Salts");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("Anonymous")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("UserName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.UserLogin", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("UserName");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.Medication", b =>
                {
                    b.HasOne("StimulationAppAPI.DAL.Model.User", "User")
                        .WithMany("Medications")
                        .HasForeignKey("Dependence")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.PasswordReset", b =>
                {
                    b.HasOne("StimulationAppAPI.DAL.Model.UserLogin", "UserLogin")
                        .WithOne("PasswordReset")
                        .HasForeignKey("StimulationAppAPI.DAL.Model.PasswordReset", "UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.Salt", b =>
                {
                    b.HasOne("StimulationAppAPI.DAL.Model.UserLogin", "UserLogin")
                        .WithOne("Salt")
                        .HasForeignKey("StimulationAppAPI.DAL.Model.Salt", "UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.UserLogin", b =>
                {
                    b.HasOne("StimulationAppAPI.DAL.Model.User", "User")
                        .WithOne("Login")
                        .HasForeignKey("StimulationAppAPI.DAL.Model.UserLogin", "UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.User", b =>
                {
                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("Medications");
                });

            modelBuilder.Entity("StimulationAppAPI.DAL.Model.UserLogin", b =>
                {
                    b.Navigation("PasswordReset");

                    b.Navigation("Salt")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
