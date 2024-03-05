using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Models;
using ErrorOr;

namespace BreakfastAPI.Services.Breakfasts;

public interface IBreakfastControllerService 
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfastByID(Guid id);
    ErrorOr<UpsertBreakfastResponse> UpsertBreakfast(Breakfast breakfast);
   
}