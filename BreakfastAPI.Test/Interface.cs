using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
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
    public class Interface
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

        #region Create Tests

        [TestMethod]
        public void Create_WithGuid_ReturnCreated()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            var errorOrBreakfast = new ErrorOr<Breakfast>();
            
            breakfastServiceMock.Setup(service => service.Create(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<TimeInterval>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<Guid>()
                    )
                    ).Returns(breakfast);
            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.Create("", "", null, null, null, Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsInstanceOfType<Breakfast>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void Create_WithoutGuid_ReturnCreated()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            var errorOrBreakfast = new ErrorOr<Breakfast>();

            breakfastServiceMock.Setup(service => service.Create(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<TimeInterval>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<List<String>>(),
                    null
                    )
                    ).Returns(breakfast);
            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.Create("", "", null, null, null);

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsInstanceOfType<Breakfast>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void Create_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.Create(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<TimeInterval>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<Guid>()
                    )
                    ).Returns(new Error());
            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.Create("", "", null, null, null, Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void Create_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.Create(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<TimeInterval>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<List<String>>(),
                    It.IsAny<Guid>()
                    )
                    ).Returns(new List<Error>() { new Error(), new Error()});

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.Create("", "", null, null, null, Guid.NewGuid());

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }

        #endregion Create Tests

        #region From Tests

        [TestMethod]
        public void From_WithCreateBreakfastRequest_ReturnBreakfast()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>())
                    ).Returns(breakfast);

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(new CreateBreakfastRequest("","",null, null,null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsInstanceOfType<Breakfast>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void From_WithCreateBreakfastRequest_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>())
                    ).Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(new CreateBreakfastRequest("", "", null, null, null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void From_WithCreateBreakfastRequest_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>())
                    ).Returns(new List<Error>() { new Error(), new Error() });

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(new CreateBreakfastRequest("", "", null, null, null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }

        [TestMethod]
        public void From_WithIdAndUpsertBreakfastRequest_ReturnBreakfast()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            var breakfast = new Breakfast(Guid.NewGuid(), "", "", null, null, null);
            breakfastServiceMock.Setup(service => service.From(
                    It.IsAny<Guid>(),
                    It.IsAny<UpsertBreakfastRequest>()
                    ))
                .Returns(breakfast);

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(Guid.NewGuid(), new UpsertBreakfastRequest("", "", null, null, null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsInstanceOfType<Breakfast>(result.Value);
            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 0);
        }

        [TestMethod]
        public void From_WithIdAndUpsertBreakfastRequest_ReturnError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.From(
                    It.IsAny<Guid>(),
                    It.IsAny<UpsertBreakfastRequest>()
                    ))
                .Returns(new Error());

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(Guid.NewGuid(), new UpsertBreakfastRequest("", "", null, null, null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count == 1);
        }

        [TestMethod]
        public void From_WithIdAndUpsertBreakfastRequest_ReturnListError()
        {
            // Arrange
            var breakfastServiceMock = new Mock<IBreakfastService>();
            breakfastServiceMock.Setup(service => service.From(
                    It.IsAny<Guid>(),
                    It.IsAny<UpsertBreakfastRequest>()
                    ))
                .Returns(new List<Error>() { new Error(), new Error() });

            var serviceUnderTest = breakfastServiceMock.Object;

            // Act
            var result = serviceUnderTest.From(Guid.NewGuid(), new UpsertBreakfastRequest("", "", null, null, null));

            // Assert
            Assert.IsInstanceOfType<ErrorOr<Breakfast>>(result);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.ErrorsOrEmptyList.Count >= 2);
        }
        #endregion From Tests

        #endregion IBreakfastService Tests
    }
}
