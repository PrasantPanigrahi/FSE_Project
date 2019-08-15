using Microsoft.EntityFrameworkCore;
using PM.Models;
using System;
namespace PM.DAL
{
    public static class DataInitializer
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "FirstName1", LastName = "LastName1", EmployeeId = "1" },
            new User { Id = 2, FirstName = "FirstName2", LastName = "LastName2", EmployeeId = "2" },
            new User { Id = 3, FirstName = "FirstName3", LastName = "LastName3", EmployeeId = "3" },
            new User { Id = 4, FirstName = "FirstName4", LastName = "LastName4", EmployeeId = "4" }
            );

            modelBuilder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "Project1", StartDate = DateTime.Parse("2019-08-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 1, ManagerId = 1 },
            new Project { Id = 2, Name = "Project2", StartDate = DateTime.Parse("2019-07-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 5, ManagerId = 1 },
            new Project { Id = 3, Name = "Project3", StartDate = DateTime.Parse("2019-06-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 2, ManagerId = 1 },
            new Project { Id = 4, Name = "Project4", StartDate = DateTime.Parse("2019-05-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 0, ManagerId = 1 }
            );

            modelBuilder.Entity<ParentTask>().HasData(
            new ParentTask { Id = 1, Name = "ParentTask1" },
            new ParentTask { Id = 2, Name = "parentTask2" }
           );

            modelBuilder.Entity<Task>().HasData(
            new Task { Id = 1, Name = "Task1", StartDate = DateTime.Parse("2019-08-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 1, OwnerId = 2, ProjectId = 1, ParentTaskId = 1 },
            new Task { Id = 2, Name = "Task2", StartDate = DateTime.Parse("2019-07-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 2, OwnerId = 2, ProjectId = 2, ParentTaskId = 1 },
            new Task { Id = 3, Name = "Task3", StartDate = DateTime.Parse("2019-06-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 3, OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new Task { Id = 4, Name = "Task4", StartDate = DateTime.Parse("2019-05-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 4, OwnerId = 4, ProjectId = 4, ParentTaskId = 1 }
             );
        }
    }
}
