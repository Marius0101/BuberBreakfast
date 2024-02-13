using ErrorOr;
using BreakfastAPI.Models;
using BreakfastAPI.Contracts.Breakfast;

namespace BreakfastAPI.Services.Breakfasts;

public class BreakfastService: IBreakfeastService
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

    public ErrorOr<Breakfast> GetBreakfast(Guid breakfastId){

        if(_breakfasts.TryGetValue(breakfastId, out var breakfast)){
            return breakfast;
        }
        return Errors.Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertBreakfastResponse> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

        _breakfasts[breakfast.Id] = breakfast;

        return new UpsertBreakfastResponse(isNewlyCreated);
    }
}