using ErrorOr;
using BreakfastAPI.Models;
using BreakfastAPI.Contracts.Breakfast;

namespace BreakfastAPI.Services.Breakfasts;

public class BreakfastControllerService: IBreakfastControllerService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfastByID(Guid id)
    {

        if(_breakfasts.TryGetValue(id, out var breakfast)){
            return breakfast;
        }
        return Errors.BreakfastErrors.NotFound;
    }

    public ErrorOr<UpsertBreakfastResponse> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

        _breakfasts[breakfast.Id] = breakfast;

        return new UpsertBreakfastResponse(isNewlyCreated);
    }
}