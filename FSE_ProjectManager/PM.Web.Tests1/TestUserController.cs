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
        public void GetUsers_ShouldReturnAllUsers()
        {
            //arrange
            var testUsers = GetTestUsers();
            var mockUserRepository = new Mock<IUserRepository>().Object;
            Mock.Get<IUserRepository>(mockUserRepository).Setup(r => r.GetAll()).Returns(testUsers);

            var userFacade = new UserFacade(mockUserRepository);
            var userController = new UserController(userFacade);

            //act
            var result =  (ObjectResult)userController.GetUserList().Result;

            //assert
            Assert.AreEqual(testUsers.Count(), ((List<UserDto>)result.Value).Count);
        }

        [Test]
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
            var result =  (ObjectResult)userController.GetUser(userIdToBeQueried).Result;

            //assert
            Assert.AreEqual(expectetUser.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(expectetUser.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
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
            var result =  (ObjectResult)userController.Update(newUserDto).Result;

            //assert
            Assert.AreEqual(newUserDto.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(newUserDto.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
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
            var result =  (ObjectResult)userController.Update(userDtoToBeUpdated).Result;

            //assert
            Assert.AreEqual(userDtoToBeUpdated.FirstName, ((UserDto)result.Value).FirstName);
            Assert.AreEqual(userDtoToBeUpdated.LastName, ((UserDto)result.Value).LastName);
        }

        [Test]
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
            var result =  (ObjectResult)userController.Delete(userIdToBDeleted).Result;

            //assert
            Assert.True(((Boolean)result.Value));
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
