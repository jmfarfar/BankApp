using BankApp.Controllers;
using BankApp.Entities;
using BankApp.Repository;
using BankApp.Tests.TestData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankApp.Tests
{
    public class BankControllerTest
    {
        private readonly BankController _bankController;

        public BankControllerTest()
        {
            //var bankRepoMock = new Mock<IBankRepository>();
            //bankRepoMock.Setup(b => b.Login("a", "a")).Returns(new Entities.User()
            //{
            //    Login = "a",
            //    Id = 1,
            //    Password = "a",
            //    Role = Entities.Roles.ADMIN,
            //    USDBalance = 300
            //});
            //_bankController = new BankController(bankRepoMock.Object);
        }

        [Fact]
        public void GetUserLogin_GetAction_MustReturnOK()
        {
            //arrange
            var bankRepoMock = new Mock<IBankRepository>();
            bankRepoMock.Setup(b => b.Login("a", "a")).Returns(new Entities.User()
            {
                Login = "a",
                Id = 1,
                Password = "a",
                Role = Entities.Roles.ADMIN,
                USDBalance = 300
            });
            var _bankController = new BankController(bankRepoMock.Object);

            //act
            var result = _bankController.Login("a", "a");

            //assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<User>(okResult.Value);
        }

        [Fact]
        public void CreateUser_CreateAction_MustReturnUser()
        {
            //arrange
            var bankRepoMock = new Mock<IBankRepository>();
            var userMock = new User()
            {
                Id = 1,
                Login = "a",
                Password = "a",
                Role = Entities.Roles.ADMIN,
                USDBalance = 100
            };

            bankRepoMock.Setup(b => b.Create(userMock));
            var _bankController = new BankController(bankRepoMock.Object);

            //act
            var result = _bankController.CreateUser(userMock);

            //assert
            var actionResult = Assert.IsType<Task<ActionResult>>(result);
            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void UpdateBalance_UpdateAction_MustReturnUserWithUpdatedBalance()
        {
            //arrange
            var bankRepoMock = new Mock<IBankRepository>();
            var user = new User()
            {
                Id = 1,
                Login = "a",
                Password = "a",
                Role = Entities.Roles.ADMIN,
                USDBalance = 100
            };

            bankRepoMock.Setup(b => b.Update(user.Login, user.Password, 200)).ReturnsAsync(new User()
            {
                Login = user.Login,
                Password = user.Password,
                Id = 1,
                Role = Entities.Roles.ADMIN,
                USDBalance = 200
            });

            var bankController = new BankController(bankRepoMock.Object);

            //act
            var result =  bankController.UpdateBalance("a", "a", 200);

            //assert
            var tActionResult = Assert.IsType<Task<ActionResult<User>>>(result);
            var actionRes = Assert.IsType<ActionResult<User>>(tActionResult.Result);
            var okObjRes = Assert.IsType<OkObjectResult>(actionRes.Result);
            var userRes = Assert.IsType<User>(okObjRes.Value);
            
            Assert.Equal("200", userRes.USDBalance.ToString());
        }

        [Fact]
        public void DeleteUser_DeleteAction_MustRemoveUser()
        {
            var repo = new UserTestDataRepository();

            var result = repo.Delete("a", "a", "b");

            Assert.True(result.Result);
        }
    }
}
