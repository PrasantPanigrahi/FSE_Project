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
    public class TestUserController
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
        public void GetUserList_ShouldReturnAllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.GetAll()).Returns(testUsers);

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (ObjectResult)userController.GetUserList().Result;

            //assert
            Assert.AreEqual(testUsers.Count(), ((List<UserDto>)result.Value).Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchUsers_ShouldReturnAllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var searchResult = new FilterResult<User>() { Data = testUsers, Total = testUsers.Count() };
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Search(It.IsAny<FilterState>())).Returns(searchResult);

            var userFacade = new UserFacade(mockUserRepository);
            var userControler = new UserController(userFacade);
            var filterState = new FilterState();

            //act : no filters
            var result = (ObjectResult)userControler.Search(filterState).Result;

            //assert
            Assert.AreEqual(testUsers.Count(), ((FilterResult<UserDto>)result.Value).Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchUser_ShouldNotReturnAnyProject()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Search(It.IsAny<FilterState>()));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act : no filters
            var result = (ObjectResult)userController.Search(null).Result;

            //assert
            Assert.Null(result.Value);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldReturnCorrectUser()
        {
            //arrange
            var userIdToBeQueried = 1;
            var testUsers = GetTestUsers();

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userIdToBeQueried)).Returns(testUsers.First(u => u.Id == userIdToBeQueried));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);
            var expectetUser = testUsers.First(u => u.Id == userIdToBeQueried);

            //act
            var result = (ObjectResult)userController.GetUser(userIdToBeQueried).Result;

            //assert
            Assert.AreEqual(expectetUser.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(expectetUser.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldNotReturnUser()
        {
            //arrange
            var userIdToBeQueried = 1000;

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userIdToBeQueried));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (StatusCodeResult)userController.GetUser(userIdToBeQueried).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldAddNewUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var newUserDto = new UserDto()
            {
                FirstName = "FirstName_Mocked",
                LastName = "LastName_Mocked",
                EmployeeId = "Mocker_Employee"
            };
            var newUser = Mapper.Map<User>(newUserDto);

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Add(newUser)).Returns(newUser);

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (ObjectResult)userController.Update(newUserDto).Result;

            //assert
            Assert.AreEqual(newUserDto.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(newUserDto.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldNotUpdateIfFirstNameAndLastNameNotProvided()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userDtoToBeUpdated = new UserDto()
            {
                Id = 4,
                EmployeeId = "Mocker_Employee"
            };

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userDtoToBeUpdated.Id));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (StatusCodeResult)userController.Update(userDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
              TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Update_ShouldUpdateCorrectUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userDtoToBeUpdated = new UserDto()
            {
                Id = 4,
                FirstName = "FirstName_Mocked",
                LastName = "LastName_Mocked",
                EmployeeId = "Mocker_Employee"
            };

            var oldUser = testUsers.First(u => u.Id == userDtoToBeUpdated.Id);

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userDtoToBeUpdated.Id)).Returns(oldUser);

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (ObjectResult)userController.Update(userDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(userDtoToBeUpdated.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(userDtoToBeUpdated.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldDeleteCorrectUser()
        {
            //arrange
            var testUsers = GetTestUsers();
            var userIdToBDeleted = 4;

            var user = testUsers.First(u => u.Id == userIdToBDeleted);

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userIdToBDeleted)).Returns(user);
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Remove(user));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (ObjectResult)userController.Delete(userIdToBDeleted).Result;

            //assert
            Assert.True(((bool)result.Value));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenUserNotFound()
        {
            //arrange
            var userToBeDeleted = 4;

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = (StatusCodeResult)userController.Delete(userToBeDeleted).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenUserHasProjects()
        {
            //arrange
            var userToBeDeleted = new User() { Id = 4, Projects = new List<Project>() { new Project() { Id = 1 } } };

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted.Id)).Returns(userToBeDeleted);

            var userFacade = new UserFacade(mockUserRepository);
            var projectController = new UserController(userFacade);

            //act
            var result = (StatusCodeResult)projectController.Delete(userToBeDeleted.Id).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void Delete_ShouldNotDeleteWhenUserHasTasks()
        {
            //arrange
            var userToBeDeleted = new User() { Id = 4, Tasks = new List<Task>() { new Task() { Id = 1 } } };

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted.Id)).Returns(userToBeDeleted);

            var userFacade = new UserFacade(mockUserRepository);
            var projectController = new UserController(userFacade);

            //act
            var result = (StatusCodeResult)projectController.Delete(userToBeDeleted.Id).Result;

            //assert
            Assert.AreEqual(500, result.StatusCode);
        }

        private IQueryable<User> GetTestUsers()
        {
            var testUsers = new List<User>
            {
            new User{Id = 1, FirstName="FirstName1",LastName="LastName1", EmployeeId = "1" },
            new User{Id = 2, FirstName="FirstName2",LastName="LastName2", EmployeeId = "2"},
            new User{Id = 3, FirstName="FirstName3",LastName="LastName3" ,EmployeeId = "3"},
            new User{Id = 4, FirstName="FirstName4",LastName="LastName4" ,EmployeeId = "4"},
            };

            return testUsers.AsQueryable();
        }
    }
}
