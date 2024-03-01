using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Models;
using BreakfastAPI.Services.Breakfasts;
using BreakfastAPI.Services.Errors;
using ErrorOr;
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
        #region IBreakfastControllerService Tests

        #region CreateBreakfast Tests
        [TestMethod]
        public void CreateBreakfast_ReturnCreated()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(),"","",null,null,null);
            breakfastServiceMock.Setup(service => service.CreateBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new ErrorOr<Created>());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.CreateBreakfast(breakfast);
            // Assert
            Assert.IsInstanceOfType<ErrorOr<Created>>(result);
            Assert.IsInstanceOfType<Created>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void CreateBreakfast_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.CreateBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.CreateBreakfast(breakfast);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Created>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void CreateBreakfast_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.CreateBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new List<Error>() { new Error(), new Error()});

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.CreateBreakfast(breakfast);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Created>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 1);
        }

        #endregion  CreateBreakfast Tests

        #region DeleteBreakfast Tests
        [TestMethod]
        public void DeleteBreakfast_ReturnDeleted()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            breakfastServiceMock.Setup(service => service.DeleteBreakfast(It.IsAny<Guid>()))
                                .Returns(new ErrorOr<Deleted>());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.DeleteBreakfast(Guid.NewGuid());
            // Assert
            Assert.IsInstanceOfType<ErrorOr<Deleted>>(result);
            Assert.IsInstanceOfType<Deleted>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void DeleteBreakfast_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            breakfastServiceMock.Setup(service => service.DeleteBreakfast(It.IsAny<Guid>()))
                                .Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.DeleteBreakfast(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Deleted>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void DeleteBreakfast_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            breakfastServiceMock.Setup(service => service.DeleteBreakfast(It.IsAny<Guid>()))
                                .Returns(new List<Error>() {new Error(), new Error()});

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.DeleteBreakfast(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Deleted>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }

        #endregion DeleteBreakfast Tests

        #region GetBreakfastByID Tests
        [TestMethod]
        public void GetBreakfastByID_ReturnBreakfast()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                                .Returns(breakfast);

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.GetBreakfastByID(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsInstanceOfType<Breakfast>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void GetBreakfastByID_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            breakfastServiceMock.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                                .Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.GetBreakfastByID(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void GetBreakfastByID_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            breakfastServiceMock.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                                .Returns(new List<Error>() { new Error(), new Error() });

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.GetBreakfastByID(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }

        #endregion GetBreakfastByID Tests

        #region UpsertBreakfast Tests
        [TestMethod]
        public void UpsertBreakfast_ReturnUpsertBreakfastResponse()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.UpsertBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new UpsertBreakfastResponse());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.UpsertBreakfast(breakfast);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<UpsertBreakfastResponse>>(result);
            Assert.IsInstanceOfType<UpsertBreakfastResponse>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void UpsertBreakfast_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.UpsertBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.UpsertBreakfast(breakfast);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<UpsertBreakfastResponse>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void UpsertBreakfast_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastControllerService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.UpsertBreakfast(It.IsAny<Breakfast>()))
                                .Returns(new List<Error>() { new Error(), new Error() });

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.UpsertBreakfast(breakfast);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<UpsertBreakfastResponse>>(result);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }
        #endregion UpsertBreakfast Tests

        #endregion IBreakfastControllerService Tests

        #region IBreakfastService Tests

        #region
        #endregion IBreakfastService Tests
    }
}
