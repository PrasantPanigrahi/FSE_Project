using PM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace PM.DAL
{
    public class DataInitializer
    {
        public static void SeedData(PMDbContext context)
        {
            var users = new List<User> {
            new User { Id = 1, FirstName = "FirstName1", LastName = "LastName1", EmployeeId = "1" },
            new User { Id = 2, FirstName = "FirstName2", LastName = "LastName2", EmployeeId = "2" },
            new User { Id = 3, FirstName = "FirstName3", LastName = "LastName3", EmployeeId = "3" },
            new User { Id = 4, FirstName = "FirstName4", LastName = "LastName4", EmployeeId = "4" },
            
        };
            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            var projects = new List<Project>
            {
            new Project { Id = 1, Name = "Project1", StartDate = DateTime.Parse("2019-08-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 1, ManagerId = 1 },
            new Project { Id = 2, Name = "Project2", StartDate = DateTime.Parse("2019-07-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 5, ManagerId = 1 },
            new Project { Id = 3, Name = "Project3", StartDate = DateTime.Parse("2019-06-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 2, ManagerId = 1 },
            new Project { Id = 4, Name = "Project4", StartDate = DateTime.Parse("2019-05-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 0, ManagerId = 1 }
             };
            projects.ForEach(p => context.Projects.AddOrUpdate(p));
            context.SaveChanges();

            var parentTask = new List<ParentTask>
            {
            new ParentTask { Id = 1, Name = "ParentTask1" },
            new ParentTask { Id = 2, Name = "parentTask2" }
           };
            parentTask.ForEach(pt => context.ParentTasks.AddOrUpdate(pt));
            context.SaveChanges();

            var tasks = new List<Task>
            {
            new Task { Id = 1, Name = "Task1", StartDate = DateTime.Parse("2019-08-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 1, OwnerId = 2, ProjectId = 1, ParentTaskId = 1 },
            new Task { Id = 2, Name = "Task2", StartDate = DateTime.Parse("2019-07-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 2, OwnerId = 2, ProjectId = 2, ParentTaskId = 1 },
            new Task { Id = 3, Name = "Task3", StartDate = DateTime.Parse("2019-06-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 3, OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new Task { Id = 4, Name = "Task4", StartDate = DateTime.Parse("2019-05-01"), EndDate = DateTime.Parse("2020-08-01"), Priority = 4, OwnerId = 4, ProjectId = 4, ParentTaskId = 1 }
             };
            tasks.ForEach(t => context.Tasks.AddOrUpdate(t));
            context.SaveChanges();
        }
    }
}
