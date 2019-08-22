using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NBench;
using NUnit.Framework;
using PM.DAL.Repositories.Interface;
using PM.Extensions;
using PM.Extensions.DTO;
using PM.Models;
using PM.Utilities;
using PM.Utilities.Filter;
using PM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PM.Web.Tests
{
    [TestFixture]
    public class TestProjectController
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
        public void GetProjects_ShouldReturnAllProjects()
        {
            //arrange
            var testProjects = GetTestProjects();
            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.GetAll()).Returns(testProjects);

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result =  (ObjectResult) projectController.GetProjects().Result;

            //assert
            Assert.AreEqual(testProjects.Count(), ((List<ProjectDto>)result.Value).Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchProjects_ShouldReturnAllProjects()
        {
            //arrange
            var testProjects = GetTestProjects();
            var queryResult = new FilterResult<Project>() { Data = testProjects, Total = testProjects.Count() };
            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Search(It.IsAny<FilterState>())).Returns(queryResult);

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);
            var filterState = new FilterState();

            //act : no filters
            var result = (ObjectResult)projectController.Search(filterState).Result;

            //assert
            Assert.AreEqual(testProjects.Count(), ((FilterResult<ProjectDto>)result.Value).Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchProjects_ShouldNotReturnAnyProject()
        {
            //arrange
            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Search(It.IsAny<FilterState>()));

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act : no filters
            var result = (ObjectResult)projectController.Search(null).Result;

            //assert
            Assert.Null(result.Value);
        }

        //[Test]
        //[PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
        //    TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        //[ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        //[MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        //[TimingMeasurement]
        //public void SearchProjects_ShouldReturnTaskWithPriorityGreaterThanAndEqualToZero()
        //{
        //    //arrange
        //    var testProjects = GetTestProjects().Where(p => p.Priority >= 0);
        //    var queryResult = new FilterResult<Project>() { Data = testProjects, Total = testProjects.Count() };
        //    var mockProjectRepository = new Mock<IProjectRepository>().Object;
        //    Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Search(It.IsAny<FilterState>())).Returns(queryResult);

        //    var projectFacade = new ProjectFacade(mockProjectRepository);
        //    var projectController = new ProjectController(projectFacade);
        //    var thisAssembly = Assembly.GetExecutingAssembly();
        //    var jsonFilePath = Path.Combine(Directory.GetParent(thisAssembly.Location).FullName, @"TestData\FilerStat.Json");
        //    var fileStatString = File.ReadAllText(jsonFilePath);
        //    var filterState = fileStatString.ToObject<FilterState>();

        //    var result = (ObjectResult)projectController.Search(filterState).Result;

        //    //assert
        //    Assert.True(((FilterResult<ProjectDto>)result.Value).Data.All(t => t.Priority >= 0));
        //}


        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetProject_ShouldReturnCorrectProject()
        {
            //arrange
            var ProjectIdToBeQueried = 1;
            var testProjects = GetTestProjects();

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBeQueried)).Returns(testProjects.First(u => u.Id == ProjectIdToBeQueried));

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);
            var expectetProject = testProjects.First(u => u.Id == ProjectIdToBeQueried);

            //act
            var result =  (ObjectResult) projectController.GetProject(ProjectIdToBeQueried).Result;

            //assert
            Assert.AreEqual(expectetProject.Name, ((ProjectDto)result.Value).Name);
            Assert.AreEqual(expectetProject.Priority, ((ProjectDto)result.Value).Priority);
            Assert.AreEqual(expectetProject.ManagerId, ((ProjectDto)result.Value).ManagerId);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetProject_ShouldNotReturnProject()
        { //arrange
            var ProjectIdToBeQueried = 1000;

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBeQueried));

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (StatusCodeResult)projectController.GetProject(ProjectIdToBeQueried).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAddNewProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var newProjectDto = new ProjectDto()
            {
                Id = 5,
                Name = "Project_5",
                StartDate = "20190101",
                EndDate = "20200901",
                Priority = 2,
                ManagerId = 2
            };
            var newProject = Mapper.Map<Project>(newProjectDto);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Add(newProject)).Returns(newProject);

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result =  (ObjectResult) projectController.Update(newProjectDto).Result;

            //assert
            Assert.AreEqual(newProjectDto.Name, ((ProjectDto)result.Value).Name);
            Assert.AreEqual(newProjectDto.Priority, ((ProjectDto)result.Value).Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
           TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdateCorrectProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var projectDtoToBeUpdated = new ProjectDto()
            {
                Id = 4,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result =  (ObjectResult) projectController.Update(projectDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(projectDtoToBeUpdated.Name, ((ProjectDto)result.Value).Name);
            Assert.AreEqual(projectDtoToBeUpdated.Priority, ((ProjectDto)result.Value).Priority);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDeleteCorrectProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var ProjectIdToBDeleted = 4;

            var Project = testProjects.First(u => u.Id == ProjectIdToBDeleted);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBDeleted)).Returns(Project);
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Remove(Project));

            var ProjectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(ProjectFacade);

            //act
            var result =  (ObjectResult) projectController.Delete(ProjectIdToBDeleted).Result;

            //assert
            Assert.True((bool)result.Value);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenProjectNotFound()
        {
            //arrange
            var ProjectIdToBDeleted = 4;

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(ProjectIdToBDeleted));

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (StatusCodeResult)projectController.Delete(ProjectIdToBDeleted).Result;

            //assert
            Assert.AreEqual(500,result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenProjectHasTasks()
        {
            //arrange
            var projectToBeDeleted = new Project() { Id = 4, Tasks = new List<Task>() { new Task() { Id = 1 } } };

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectToBeDeleted.Id)).Returns(projectToBeDeleted);

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (StatusCodeResult)projectController.Delete(projectToBeDeleted.Id).Result;

            //assert
            Assert.AreEqual(500,result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_WhenProjectDoesNotExists()
        {
            //arrange
            var testProjects = GetTestProjects();
            var projectDtoToBeUpdated = new ProjectDto()
            {
                Id = -1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                ManagerId = 2
            };

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id));

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (StatusCodeResult) projectController.UpdateProjectState(projectDtoToBeUpdated).Result;

            //assert
            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_WhenProjectAlreadyUpdated()
        {
            //arrange
            var testProjects = GetTestProjects();
            var projectDtoToBeUpdated = new ProjectDto()
            {
                Id = 1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                IsSuspended = false,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (StatusCodeResult) projectController.UpdateProjectState(projectDtoToBeUpdated).Result;

            //assert
            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void UpdateProjectState_ShouldUpdateCorrectProject()
        {
            //arrange
            var testProjects = GetTestProjects();
            var projectDtoToBeUpdated = new ProjectDto()
            {
                Id = 1,
                Name = "Project_5_updated",
                StartDate = "20190101",
                EndDate = "20210901",
                Priority = 2,
                IsSuspended = true,
                ManagerId = 2
            };

            var oldProject = testProjects.First(u => u.Id == projectDtoToBeUpdated.Id);

            var mockProjectRepository = new Mock<IProjectRepository>().Object;
            Mock.Get<IProjectRepository>(mockProjectRepository).Setup(r => r.Get(projectDtoToBeUpdated.Id)).Returns(oldProject);

            var projectFacade = new ProjectFacade(mockProjectRepository);
            var projectController = new ProjectController(projectFacade);

            //act
            var result = (ObjectResult)projectController.UpdateProjectState(projectDtoToBeUpdated).Result;

            //assert
            //assert
            Assert.True(((bool)result.Value));
        }

        private IQueryable<Project> GetTestProjects()
        {
            var testProjects = new List<Project>
            {
            new Project { Id =1, Name = "Project1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1, IsSuspended = false},
            new Project { Id = 2, Name = "Project2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1, IsSuspended = false },
            new Project { Id = 3, Name = "Project3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 10, ManagerId = 1, IsSuspended = false },
            new Project { Id =4, Name = "Project4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 15, ManagerId = 1 , IsSuspended = false},
            };

            return testProjects.AsQueryable();
        }
    }
}
