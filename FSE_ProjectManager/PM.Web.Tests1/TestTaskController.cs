using AutoMapper;
using Moq;
using NBench;
using NUnit.Framework;
using PM.DAL.Repositories.Interface;
using PM.Extensions.DTO;
using PM.Extensions.Interfaces;
using PM.Models;
using PM.Utilities;
using PM.Utilities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using PM.Web.Controllers;
using PM.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace PM.Web.Tests
{
    [TestFixture]
    public class TestTaskController
    {
        [SetUp]
        [PerfSetup]
        public void InitializeOneTimeData()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Initialize();
        }

        [Test]
        public void GetTasks_ShouldReturnAllTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
           var result =  (ObjectResult)TaskController.GetTasks().Result;

            //assert
            Assert.AreEqual(testTasks.Count(), ((List<TaskDto>)result.Value).Count);
        }

        [Test]
        public void GetTask_ShouldReturnCorrectTask()
        {
            //arrange
            var TaskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried)).Returns(testTasks.First(u => u.Id == TaskIdToBeQueried));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);
            var expectetTask = testTasks.First(u => u.Id == TaskIdToBeQueried);

            //act
           var result =  (ObjectResult)TaskController.GetTask(TaskIdToBeQueried).Result;

            //assert
            Assert.AreEqual(expectetTask.Name, ((TaskDto)result.Value).Name);
            Assert.AreEqual(expectetTask.Priority, ((TaskDto)result.Value).Priority);
        }

        [Test]
        public void Update_ShouldAddNewTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var newTaskDto = new TaskDto()
            {
                Id = 5,
                Name = "Task_5",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };
            var newTask = Mapper.Map<Task>(newTaskDto);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Add(newTask)).Returns(newTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
           var result =  (ObjectResult)TaskController.Update(newTaskDto).Result;

            //assert
            Assert.AreEqual(newTaskDto.Name, ((TaskDto)result.Value).Name);
            Assert.AreEqual(newTaskDto.Priority, ((TaskDto)result.Value).Priority);
        }

        [Test]
        public void Update_ShouldUpdateCorrectTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var TaskDtoToBeUpdated = new TaskDto()
            {
                Id = 4,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };

            var oldTask = testTasks.First(u => u.Id == TaskDtoToBeUpdated.Id);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.Id)).Returns(oldTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
           var result =  (ObjectResult)TaskController.Update(TaskDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(TaskDtoToBeUpdated.Name, ((TaskDto)result.Value).Name);
            Assert.AreEqual(TaskDtoToBeUpdated.Priority, ((TaskDto)result.Value).Priority);
        }

        [Test]
        public void Delete_ShouldDeleteCorrectTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var TaskIdToBDeleted = 4;

            var Task = testTasks.First(u => u.Id == TaskIdToBDeleted);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted)).Returns(Task);
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Remove(Task));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var TaskController = new TaskController(TaskFacade);

            //act
           var result =  (ObjectResult)TaskController.Delete(TaskIdToBDeleted).Result;

            //assert
            Assert.True(((bool)result.Value));
        }

        private IQueryable<Models.Task> GetTestTasks()
        {
            var testTasks = new List<Models.Task>
            {
             new Models.Task { Id = 1, Name = "Task_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1},
            new Models.Task { Id = 2, Name = "Task_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1},
            new Models.Task { Id = 2, Name = "Task_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1 },
            new Models.Task { Id = 4, Name = "Task_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1 },
            };

            return testTasks.AsQueryable();
        }
    }
}
