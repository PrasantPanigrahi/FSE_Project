using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NBench;
using NUnit.Framework;
using PM.DAL.Repositories.Interface;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Models;
using PM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Web.Tests
{
    [TestFixture]
    public class TestParentTaskController
    {
        [SetUp]
        [PerfSetup]
        public void InitializeOneTimeData()
        {
            AutoMapper.Mapper.Reset();
            AutoMapperConfig.Initialize();
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTasks_ShouldReturnAllParenTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);

            var taskFacade = new ParentTaskFacade(mockTaskRepository);
            var taskController = new ParentTaskController(taskFacade);
            //((Microsoft.AspNetCore.Mvc.ObjectResult)taskController.GetTasks().Result).Value

            //act
            var objResult = (ObjectResult)taskController.GetTasks().Result;
            var result = objResult.Value as List<ParentTaskDto>;           

            //assert
            Assert.AreEqual(testTasks.Count(), result.Count);
        }

        [Test]
        public void GetUser_ShouldReturnCorrectUser()
        {
            //arrange
            var taskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried)).Returns(testTasks.First(u => u.Id == taskIdToBeQueried));

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);
            var expectetUser = testTasks.First(u => u.Id == taskIdToBeQueried);

            //act
            var result = (ObjectResult)taskController.GetTask(taskIdToBeQueried).Result;

            //assert
            Assert.AreEqual(expectetUser.Name, ((ParentTaskDto)result.Value).Name);
        }

        [Test]
        public void Update_ShouldAddNewUser()
        {
            //arrange
            var testTasks = GetTestTasks();
            var newTaskDto = new ParentTaskDto()
            {
                Name = "Name_Mocked",
            };
            var newUser = Mapper.Map<ParentTask>(newTaskDto);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Add(newUser)).Returns(newUser);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = (ObjectResult)taskController.Update(newTaskDto).Result;

            //assert
            Assert.AreEqual(newTaskDto.Name, ((ParentTaskDto)result.Value).Name);
        }

        [Test]
        public void Update_ShouldUpdateCorrectUser()
        {
            //arrange
            var testTasks = GetTestTasks();
            var userDtoToBeUpdated = new ParentTaskDto()
            {
                Id = 2,
                Name = "Name_updated"
            };

            var oldTask = testTasks.First(u => u.Id == userDtoToBeUpdated.Id);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(userDtoToBeUpdated.Id)).Returns(oldTask);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = (ObjectResult)taskController.Update(userDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(userDtoToBeUpdated.Name, ((ParentTaskDto)result.Value).Name);
        }


        private IQueryable<ParentTask> GetTestTasks()
        {
            var parentTask = new List<ParentTask>
            {
            new ParentTask { Id =1, Name = "ParentTask_1"},
            new ParentTask { Id =2, Name = "parentTask_2" },
            };

            return parentTask.AsQueryable();
        }
    }
}
