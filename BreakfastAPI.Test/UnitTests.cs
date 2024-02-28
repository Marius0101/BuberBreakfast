using BreakfastAPI.Contracts.Breakfast;
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
            var mockService = new Mock<IBreakfastControllerService>();
            mockService.Setup(service => service.GetBreakfastByID(It.IsAny<Guid>()))
                       .Returns(Breakfast.Create(
                           name: "Test",
                           description : "A really long sentence for my 50 long description.",
                           availability : null,
                           savory : null,
                           sweet : null));
            var controller = new BreakfastControllers(mockService.Object);

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
                                                .Returns(Errors.Breakfast.NotFound);
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
            var mockService = new Mock<IBreakfastControllerService>();
            var request = new CreateBreakfastRequest
            (
                Name:        "Test",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory:       null,
                Sweet:        null
            );   
            var controller = new BreakfastControllers(mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsInstanceOfType<BreakfastResponse>(createdAtActionResult.Value);
            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
        }

        [TestMethod]
        public void CreateBreakfastWithShortName_ReturnValidationProblem()
        {
            var mockService = new Mock<IBreakfastControllerService>();
            var request = new CreateBreakfastRequest
            (
                Name: "Te",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory: null,
                Sweet: null
            );
            var controller = new BreakfastControllers(mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsInstanceOfType<ValidationProblemDetails>(objectResult.Value);
        }
        [TestMethod]
        public void CreateBreakfastWithLongName_ReturnValidationProblem()
        {
            var mockService = new Mock<IBreakfastControllerService>();
            var mockValidationResult = new Mock<Breakfast>();
            
            var request = new CreateBreakfastRequest
            (
                Name: "Te",
                Description: "A really long sentence for my 50 long description.",
                Availability: null,
                Savory: null,
                Sweet: null
            );
            var controller = new BreakfastControllers(mockService.Object);

            var result = controller.CreateBreakfast(request);

            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsInstanceOfType<ValidationProblemDetails>(objectResult.Value);
        }
    }
}