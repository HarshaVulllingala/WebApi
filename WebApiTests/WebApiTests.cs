using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

using System.Collections.Generic;
using WebApplMVC5.Controllers;
using WebApplMVC5;
using WebApi.Services;
using WebApi.Repositories;
using WebApi.Models;

namespace WebApiTests
{
    [TestClass]
    public class WebApiTests
    {
        public WebApiTests()
        {

        }
        //x unit -[Fact]
        //n uint -[Test] ; class [testfixer]

        //[TestMethod]

        //public void Unitofwork_stateundertest_expectedbehaviour()
        //{

        //}
        //[TestMethod]
        //public void GetUsers_getuserscount_returnListofUsers()
        //{
        //    //Arrange
        //    List<User> users = new List<User>() {
        //    new User { id=6, name="sanjay", city="vizag", contact="9912343424"}, 
        //    new User { id=7, name = "krishna", city = "Hyd", contact = "9912543434"},
        //    new User { id=8, name = "Robert", city = "Goa", contact = "9912143143"}
        //    };
        //    //Act

        //    //Assert
        //}
        //[TestMethod]
        //public void GetUser_ById_whenuserExists()
        //{
        //    //Arrange
        //    User mockuser = new User { id = 5, name = "manikanta", city = "vijayawada", contact = "9988776655" };
        //    HomeController controller = new HomeController();
        //    //Act
        //    var user = controller.GetUser(mockuser.id);
        //    //Assert
        //    Assert.AreEqual(mockuser.name,user.name);
        //}
        [TestMethod]
        public void Divide_twonumber_positiveinteger()
        {
            //Arrange
            int a = 10;
            int b = 2;
            int expected = 5;
            Calculate cal = new Calculate();
            //Act
            double result = cal.Divide(a,b);

            //Assert
            Assert.AreEqual(result,expected);
        }
        [TestMethod]
        public void Divide_twonumber_negetivedenominator()
        {
            //Arrange
            int a = 10;
            int b = -2;
            int expected = -5;
            Calculate cal = new Calculate();
            //Act
            double result = cal.Divide(a, b);

            //Assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void GetUserbyID_AnyId_ReturnsNull()
        {
            var user = new Mock<IUserService>();
            var mockrepo = new Mock<IUserrepository>();
            
            user.Setup(x => x.GetUserById(10)).Returns(() => null);
            var service = new UserService(mockrepo.Object);

            User result = service.GetUserById(10);
            
            Assert.IsNull(result);
                
          }


        
    }
}
