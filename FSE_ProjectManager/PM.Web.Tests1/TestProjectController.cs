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

        private IQueryable<Project> GetTestProjects()
        {
            var testProjects = new List<Project>
            {
            new Project { Id =1, Name = "Project_1",  StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 1 , ManagerId = 1},
            new Project { Id = 2, Name = "Project_2",  StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 5, ManagerId = 1 },
            new Project { Id = 3, Name = "Project_3",  StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 10, ManagerId = 1 },
            new Project { Id =4, Name = "Project_4",  StartDate = DateTime.Parse("2019-04-01"), EndDate = DateTime.Parse("2021-09-01"), Priority = 15, ManagerId = 1 },
            };

            return testProjects.AsQueryable();
        }
    }
}
