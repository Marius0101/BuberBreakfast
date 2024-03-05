using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Controllers;
using BreakfastAPI.Models;
using BreakfastAPI.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakfastAPI.UnitTest
{
    [TestClass]
    public class Service
    {
        #region BreakfastControllerService Tests

        #region CreateBreakfast Tests

        [TestMethod]
        public void CreateBreakfast_ShouldReturnCreated()
        {
            // Arrange
            var service = new BreakfastControllerService();
            var breakfast = new Breakfast
            (
                Guid.NewGuid(),
                "",
                "",
                null,
                null,
                null
            );

            //Act
            var result = service.CreateBreakfast( breakfast );

            //Assert
            var expectedId = service.GetBreakfastByID(breakfast.Id).Value.Id;
            Assert.IsInstanceOfType<ErrorOr<Created>>(result);
            Assert.IsFalse(result.IsError);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType<Created>(result.Value);
            Assert.AreEqual(expectedId,breakfast.Id);
        }

        [TestMethod]
        public void CreateBreakfast_WithAnExisitingGuid_ShouldReturnError()
        {
            // Arrange
            var service = new BreakfastControllerService();
            var breakfast = new Breakfast
            (
                Guid.NewGuid(),
                "",
                "",
                null,
                null,
                null
            );
            service.CreateBreakfast(breakfast);

            //Act
            var result = service.CreateBreakfast(breakfast);

            //Assert
            Assert.IsInstanceOfType<ErrorOr<Created>>(result);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("Breakfast.Id.Conflict", result.FirstError.Code);
            Assert.AreEqual("The guid id is already in database", result.FirstError.Description);
        }

        #endregion CreateBreakfast Tests

        #region DeleteBreakfast Tests

        #endregion DeleteBreakfast Tests

        #endregion  BreakfastControllerService Tests
    }
}
