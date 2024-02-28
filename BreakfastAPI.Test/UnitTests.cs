using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Controllers;
using BreakfastAPI.Models;
using BreakfastAPI.Services.Breakfasts;
using BreakfastAPI.Services.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Xml.Linq;

namespace BreakfastAPI.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetBreakfastByID_Return200WithItem()
        {
            var mockServiceController = new Mock<IBreakfastControllerService>();
            var mockService = new Mock<IBreakfastService>();

            mockService.Setup(service =>service.Create(
                "Test",
                "A really long sentence for my 50 long description.",
                null,
                null,
                null,
                It.IsAny<Guid>()
            )).Returns((string name, string description, TimeInterval availability, List<string> savory, List<string> sweet, Guid? id) =>
            new Breakfast(
                 id ?? Guid.Empty, // Utilizați id-ul primit sau un Guid gol
                 name,
                 description,
                availability,
                savory,
                sweet
            ));
            mockServiceController.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                       .Returns((Guid id) => mockService.Object.Create(
                           name: "Test",
                           description: "A really long sentence for my 50 long description.",
                           availability: null,
                           savory: null,
                           sweet: null,
                           id: id
                           ));
        
            var controller = new BreakfastControllers(mockServiceController.Object, mockService.Object);

            var result = controller.GetBreakfastByID(Guid.NewGuid());

            Assert.IsInstanceOfType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType<BreakfastResponse>(okResult.Value);
        }

        [TestMethod]
        public void GetBreakfastByID_Return404()
        {
            var mockService = new Mock<IBreakfastControllerService>();
            mockService.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                                                .Returns(BreakfastErrors.NotFound);
            var controller = new BreakfastControllers(mockService.Object);

            var result = controller.GetBreakfastByID(Guid.NewGuid());

            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsInstanceOfType<ProblemDetails>(objectResult.Value);
            var problemDetails = objectResult.Value as ProblemDetails;
            Assert.AreEqual(StatusCodes.Status404NotFound, problemDetails.Status);
            Assert.AreEqual("The breakfast was not found in the database.", problemDetails.Title);
        }

        [TestMethod]
        public void CreateBreakfastCorrectly_Return201WithBreakfastResponse()
        {
            var mockServiceController = new Mock<IBreakfastControllerService>();
            var mockService = new Mock<IBreakfastService>();
            mockService.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>()))
                .Returns((CreateBreakfastRequest request) =>
                {
                    var breakfast = new Breakfast(
                    Guid.NewGuid(),
                    request.Name,
                    request.Description,
                    request.Availability,
                    request.Savory,
                    request.Sweet
                    );
                    return breakfast;
                });
            var request = new CreateBreakfastRequest
            (
                Name:        "Test",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory:       null,
                Sweet:        null
            );   
            var controller = new BreakfastControllers(mockServiceController.Object,mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsInstanceOfType<BreakfastResponse>(createdAtActionResult.Value);
            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
        }

        [TestMethod]
        public void CreateBreakfastWithInvalidName_ReturnValidationProblem()
        {
            var mockServiceController = new Mock<IBreakfastControllerService>();
            var mockService = new Mock<IBreakfastService>();
            mockService.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>()))
                .Returns(BreakfastErrors.InvalidName);
            var request = new CreateBreakfastRequest
            (
                Name: "Te",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory: null,
                Sweet: null
            );
            var controller = new BreakfastControllers(mockServiceController.Object,mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsInstanceOfType<ValidationProblemDetails>(objectResult.Value);
        }

        [TestMethod]
        public void CreateBreakfastWithInvalidDescription_ReturnValidationProblem()
        {
            var mockServiceController = new Mock<IBreakfastControllerService>();
            var mockService = new Mock<IBreakfastService>();
            mockService.Setup(service => service.From(It.IsAny<CreateBreakfastRequest>()))
                .Returns(BreakfastErrors.InvalidDescription);

            var request = new CreateBreakfastRequest
            (
                Name: "Te",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory: null,
                Sweet: null
            );
            var controller = new BreakfastControllers(mockServiceController.Object, mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsInstanceOfType<ValidationProblemDetails>(objectResult.Value);
        }
    }
}