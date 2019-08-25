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
        public void GetUsers_ShouldReturnAllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.GetAll()).Returns(testUsers);
            
            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result = userController.GetUsers() as OkNegotiatedContentResult<List<UserDto>>;

            //assert
            Assert.AreEqual(testUsers.Count(), result.Content.Count);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryUsers_ShouldReturnAllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var queryResult = new FilterResult<User>() { Data = testUsers, Total = testUsers.Count() };
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Search(It.IsAny<FilterState>())).Returns(queryResult);

            var userFacade = new UserFacade(mockUserRepository);
            var userControler = new UserController(userFacade);
            var filterState = new FilterState();

            //act : no filters
            var x = userControler.Search(filterState);
            var result = x as OkNegotiatedContentResult<FilterResult<UserDto>>;

            //assert
            Assert.AreEqual(testUsers.Count(), result.Content.Total);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void QueryUser_ShouldNotReturnAnyProject()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Search(It.IsAny<FilterState>()));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act : no filters
            var result = userController.Search(null) as OkNegotiatedContentResult<FilterResult<ProjectDto>>;

            //assert
            Assert.Null(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void  GetUser_ShouldReturnCorrectUser()
        {
            //arrange
            var userIdToBeQueried = 1;
            var testUsers = GetTestUsers();

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userIdToBeQueried)).Returns(testUsers.First(u=>u.Id == userIdToBeQueried));

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);
            var expectetUser = testUsers.First(u => u.Id == userIdToBeQueried);

            //act
            var result = userController.GetUser(userIdToBeQueried) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.AreEqual(expectetUser.FirstName, result.Content.FirstName);
            Assert.AreEqual(expectetUser.LastName, result.Content.LastName);
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
            var result = userController.GetUser(userIdToBeQueried) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.AreEqual(null, result); 
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void GetUser_ShouldNotReturnUser_DB()
        {
            //arrange
            var userIdToBeQueried = -1;
            var userController = new UserController();

            //act
            var result = userController.GetUser(userIdToBeQueried) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.AreEqual(null, result);
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
            var newUserDto = new UserDto() {
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
            var result = userController.Update(newUserDto) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.AreEqual(newUserDto.FirstName, result.Content.FirstName);
            Assert.AreEqual(newUserDto.LastName, result.Content.LastName);
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
            var result = userController.Update(userDtoToBeUpdated) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.IsNull(result);
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
            var result = userController.Update(userDtoToBeUpdated) as OkNegotiatedContentResult<UserDto>;

            //assert
            Assert.AreEqual(userDtoToBeUpdated.FirstName, result.Content.FirstName);
            Assert.AreEqual(userDtoToBeUpdated.LastName, result.Content.LastName);
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
            var result = userController.Delete(userIdToBDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.True(result.Content); 
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
            var result = userController.Delete(userToBeDeleted) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
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
            var userToBeDeleted = new User() { Id = 4, Projects = new List<Project>() { new Project() {Id= 1 } }};

            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.Get(userToBeDeleted.Id)).Returns(userToBeDeleted);

            var userFacade = new UserFacade(mockUserRepository);
            var projectController = new UserController(userFacade);

            //act
            var result = projectController.Delete(userToBeDeleted.Id) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
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
            var result = projectController.Delete(userToBeDeleted.Id) as OkNegotiatedContentResult<bool>;

            //assert
            Assert.Null(result);
        }

        private IQueryable<User> GetTestUsers()
        {
            var testUsers = new List<User>
            {
            new User{Id = 1, FirstName="FirstName_1",LastName="LastName_1", EmployeeId = "1" },
            new User{Id = 2, FirstName="FirstName_2",LastName="LastName_2", EmployeeId = "2"},
            new User{Id = 3, FirstName="FirstName_3",LastName="LastName_3" ,EmployeeId = "3"},
            new User{Id = 4, FirstName="FirstName_4",LastName="LastName_4" ,EmployeeId = "4"},
            };

            return testUsers.AsQueryable();
        }
    }
}