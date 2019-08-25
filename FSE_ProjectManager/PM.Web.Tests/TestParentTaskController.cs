using AutoMapper;
using Moq;
using NBench;
using NUnit.Framework;
using PM.DAL.Repositories.Interface;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Models;
using PM.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

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
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTasks_ShouldReturnAllParenTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);

            var taskFacade = new ParentTaskFacade(mockTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.GetTasks() as OkNegotiatedContentResult<List<ParentTaskDto>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetparentTask_ShouldReturnCorrectparentTask()
        {
            //arrange
            var taskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried)).Returns(testTasks.First(u => u.Id == taskIdToBeQueried));

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);
            var expectetparentTask = testTasks.First(u => u.Id == taskIdToBeQueried);

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(expectetparentTask.Name, result.Content.Name);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
              TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetparentTask_ShouldNotReturnTask()
        {
            //arrange
            var taskIdToBeQueried = 1000;

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(taskIdToBeQueried));

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(result, null);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetparentTask_ShouldNotReturnTask_DB()
        {
            //arrange
            var taskIdToBeQueried = -1;
            var taskController = new ParentTaskController();

            //act
            var result = taskController.GetTask(taskIdToBeQueried) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(result, null);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAddNewparentTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var newTaskDto = new ParentTaskDto()
            {
                Name = "Name_Mocked",
            };
            var newparentTask = Mapper.Map<ParentTask>(newTaskDto);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Add(newparentTask)).Returns(newparentTask);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(newTaskDto) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdateCorrectParentTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var parentTaskDtoToBeUpdated = new ParentTaskDto()
            {
                Id = 2,
                Name = "Name_updated"
            };

            var oldTask = testTasks.First(u => u.Id == parentTaskDtoToBeUpdated.Id);

            var mockParentTaskRepository = new Mock<IParentTaskRepository>().Object;
            Mock.Get<IParentTaskRepository>(mockParentTaskRepository).Setup(r => r.Get(parentTaskDtoToBeUpdated.Id)).Returns(oldTask);

            var taskFacade = new ParentTaskFacade(mockParentTaskRepository);
            var taskController = new ParentTaskController(taskFacade);

            //act
            var result = taskController.Update(parentTaskDtoToBeUpdated) as OkNegotiatedContentResult<ParentTaskDto>;

            //assert
            Assert.AreEqual(parentTaskDtoToBeUpdated.Name, result.Content.Name);
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