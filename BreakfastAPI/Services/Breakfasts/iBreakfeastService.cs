using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Models;
using ErrorOr;

namespace BreakfastAPI.Services.Breakfasts;

public interface IBreakfeastService 
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertBreakfastResponse> UpsertBreakfast(Breakfast breakfast);
}