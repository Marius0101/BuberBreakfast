
using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Models;
using BreakfastAPI.Services.Breakfasts;
using BreakfastAPI.Services.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BreakfastAPI.Controllers
{
    [Route("breakfast")]
    public class BreakfastControllers : ApiController
    {
        private readonly IBreakfeastService _breakfastService;

        public BreakfastControllers(IBreakfeastService breakfastService){
            _breakfastService= breakfastService;
        }

        #region Endpoints

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            var validationBreakfastResult = Breakfast.From(request);

            if (validationBreakfastResult.IsError){
                return Problem(validationBreakfastResult.Errors);
            }
            var breakfast = validationBreakfastResult.Value;

            //TODO : save breakfast to a real database 
            var createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);
            
            return createBreakfastResult.Match(
                created => CreateATGetBreakfeast(breakfast),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            var getBreakfastResult = _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                breakfeast =>Ok(MapBreakfastResponse(breakfeast)),
                errors => Problem(errors));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request)
        {
            var validationBreakfastResult = Breakfast.From(id ,request);
            
            if (validationBreakfastResult.IsError)
            {
                return Problem(errors: validationBreakfastResult.Errors);
            }
            var breakfast = validationBreakfastResult.Value;
            var upsertResponse = _breakfastService.UpsertBreakfast(breakfast);

            return upsertResponse.Match(
                response => response.isNewlyCreated ? CreateATGetBreakfeast(breakfast) : NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            var deleteResult =_breakfastService.DeleteBreakfast(id);

            return deleteResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }

        #endregion Endpoints

        #region Methods
        public static BreakfastResponse MapBreakfastResponse(Breakfast breakfast){

            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.Availability,
                breakfast.Savory,
                breakfast.Sweet
            );

            return response;
        }
        #endregion

        [HttpGet]
        public CreatedAtActionResult CreateATGetBreakfeast(Breakfast breakfast){
            return CreatedAtAction(
                actionName:  nameof(GetBreakfast),
                routeValues: new { id = breakfast.Id },
                value:       MapBreakfastResponse(breakfast)
                );
        }
    }
}