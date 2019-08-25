using AutoMapper;
using Moq;
using NBench;
using NUnit.Framework;
using PM.DAL.Repositories.Interface;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Models;
using PM.Utilities.Filter;
using PM.Web;
using PM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

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
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTasks_ShouldReturnAllTasks()
        {
            //arrange
            var testTasks = GetTestTasks();
            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.GetAll()).Returns(testTasks);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.GetTasks() as OkNegotiatedContentResult<List<TaskDto>>;

            //assert
            Assert.AreEqual(testTasks.Count(), result.Content.Count);
        }

        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        [Test]
        public void QueryTasks_ShouldReturnAllProjects()
        {
            //arrange
            var testProjects = GetTestTasks();
            var queryResult = new FilterResult<Task>() { Data = testProjects, Total = testProjects.Count() };
            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Search(It.IsAny<FilterState>())).Returns(queryResult);

            var taskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(taskFacade);
            var filterState = new FilterState();

            //act : no filters
            var x = taskController.Search(filterState);
            var result = x as OkNegotiatedContentResult<FilterResult<TaskDto>>;

            //assert
            Assert.AreEqual(testProjects.Count(), result.Content.Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryTasks_ShouldNotReturnAnyTask()
        {
            //arrange
            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Search(It.IsAny<FilterState>()));

            var taskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(taskFacade);
            var filterState = new FilterState();

            //act : no filters
            var x = taskController.Search(filterState);
            var result = x as OkNegotiatedContentResult<FilterResult<TaskDto>>;

            //assert
            Assert.IsNull(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTask_ShouldReturnCorrectTask()
        {
            //arrange
            var TaskIdToBeQueried = 1;
            var testTasks = GetTestTasks();

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried)).Returns(testTasks.First(u => u.Id == TaskIdToBeQueried));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);
            var expectetTask = testTasks.First(u => u.Id == TaskIdToBeQueried);

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(expectetTask.Name, result.Content.Name);
            Assert.AreEqual(expectetTask.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTask_ShouldNotReturnTask()
        {
            //arrange
            var TaskIdToBeQueried = 1000;

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBeQueried));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetTask_ShouldNotReturnTask_DB()
        {
            //arrange
            var TaskIdToBeQueried = -1;
            var taskController = new TaskController();

            //act
            var result = taskController.GetTask(TaskIdToBeQueried) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
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
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.Update(newTaskDto) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(newTaskDto.Name, result.Content.Name);
            Assert.AreEqual(newTaskDto.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
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
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.Update(TaskDtoToBeUpdated) as OkNegotiatedContentResult<TaskDto>;

            //assert
            Assert.AreEqual(TaskDtoToBeUpdated.Name, result.Content.Name);
            Assert.AreEqual(TaskDtoToBeUpdated.Priority, result.Content.Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDeleteCorrectTask()
        {
            //arrange
            var testTasks = GetTestTasks();
            var TaskIdToBDeleted = 4;

            var task = testTasks.First(u => u.Id == TaskIdToBDeleted);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted)).Returns(task);
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Remove(task));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.Delete(TaskIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenNoTaskFound()
        {
            //arrange
            var TaskIdToBDeleted = 4;

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskIdToBDeleted));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.Delete(TaskIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.IsNull(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_WhenTaskDoesNotExist()
        {
            //arrange
            var TaskDtoToBeUpdated = new TaskDto()
            {
                Id = -1,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1
            };

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.Id));

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.UpdateTaskStatus(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_WhenTaskAlreadyUpToDate()
        {
            //arrange
            var TaskDtoToBeUpdated = new TaskDto()
            {
                Id = 1,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1,
                StatusId = 2
            };

            var testTasks = GetTestTasks();
            var oldTask = testTasks.First(u => u.Id == TaskDtoToBeUpdated.Id);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.Id)).Returns(oldTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.UpdateTaskStatus(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(null, result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateTaskState_ShouldUpdateCorrectTask()
        {
            //arrange
            var TaskDtoToBeUpdated = new TaskDto()
            {
                Id = 1,
                Name = "Task_4_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 1,
                OwnerId = 2,
                ProjectId = 1,
                ParentTaskId = 1,
                StatusId = 3
            };

            var testTasks = GetTestTasks();
            var oldTask = testTasks.First(u => u.Id == TaskDtoToBeUpdated.Id);

            var mockTaskRepository = new Mock<ITaskRepository>().Object;
            Mock.Get<ITaskRepository>(mockTaskRepository).Setup(r => r.Get(TaskDtoToBeUpdated.Id)).Returns(oldTask);

            var TaskFacade = new TaskFacade(mockTaskRepository);
            var taskController = new TaskController(TaskFacade);

            //act
            var result = taskController.UpdateTaskStatus(TaskDtoToBeUpdated) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content);
        }

        private IQueryable<Task> GetTestTasks()
        {
            var testTasks = new List<Task>
            {
             new Task { Id = 1, Name = "Task_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , OwnerId = 2, ProjectId = 1, ParentTaskId = 1, StatusId = 2},
            new Task { Id = 2, Name = "Task_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 2 , OwnerId = 2, ProjectId = 2, ParentTaskId = 1, StatusId = 2},
            new Task { Id = 2, Name = "Task_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 3 , OwnerId = 3, ProjectId = 3, ParentTaskId = 1, StatusId = 2 },
            new Task { Id = 4, Name = "Task_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 4 , OwnerId = 4, ProjectId = 4, ParentTaskId = 1, StatusId = 2 },
            };

            return testTasks.AsQueryable();
        }
    }
}