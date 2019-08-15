﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PM.DAL;

namespace PM.DAL.Migrations
{
    [DbContext(typeof(PMDbContext))]
    [Migration("20190815132401_Migration_v0.1")]
    partial class Migration_v01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PM.Models.ParentTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ParentTasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ParentTask1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "parentTask2"
                        });
                });

            modelBuilder.Entity("PM.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsSuspended");

                    b.Property<int>("ManagerId");

                    b.Property<string>("Name");

                    b.Property<int>("Priority");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsSuspended = false,
                            ManagerId = 1,
                            Name = "Project1",
                            Priority = 1,
                            StartDate = new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsSuspended = false,
                            ManagerId = 1,
                            Name = "Project2",
                            Priority = 5,
                            StartDate = new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsSuspended = false,
                            ManagerId = 1,
                            Name = "Project3",
                            Priority = 2,
                            StartDate = new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsSuspended = false,
                            ManagerId = 1,
                            Name = "Project4",
                            Priority = 0,
                            StartDate = new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("PM.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<int?>("ParentTaskId");

                    b.Property<int>("Priority");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentTaskId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Task1",
                            OwnerId = 2,
                            ParentTaskId = 1,
                            Priority = 1,
                            ProjectId = 1,
                            StartDate = new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 0
                        },
                        new
                        {
                            Id = 2,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Task2",
                            OwnerId = 2,
                            ParentTaskId = 1,
                            Priority = 2,
                            ProjectId = 2,
                            StartDate = new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 0
                        },
                        new
                        {
                            Id = 3,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Task3",
                            OwnerId = 3,
                            ParentTaskId = 1,
                            Priority = 3,
                            ProjectId = 3,
                            StartDate = new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 0
                        },
                        new
                        {
                            Id = 4,
                            EndDate = new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Task4",
                            OwnerId = 4,
                            ParentTaskId = 1,
                            Priority = 4,
                            ProjectId = 4,
                            StartDate = new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 0
                        });
                });

            modelBuilder.Entity("PM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = "1",
                            FirstName = "FirstName1",
                            LastName = "LastName1"
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = "2",
                            FirstName = "FirstName2",
                            LastName = "LastName2"
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = "3",
                            FirstName = "FirstName3",
                            LastName = "LastName3"
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = "4",
                            FirstName = "FirstName4",
                            LastName = "LastName4"
                        });
                });

            modelBuilder.Entity("PM.Models.Project", b =>
                {
                    b.HasOne("PM.Models.User", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PM.Models.Task", b =>
                {
                    b.HasOne("PM.Models.User", "Owner")
                        .WithMany("Tasks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PM.Models.ParentTask", "ParentTask")
                        .WithMany("Tasks")
                        .HasForeignKey("ParentTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PM.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
