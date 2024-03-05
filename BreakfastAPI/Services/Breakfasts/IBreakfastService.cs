using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Models;
using ErrorOr;

namespace BreakfastAPI.Services.Breakfasts
{
    public interface IBreakfastService
    {
        ErrorOr<Breakfast> Create(
            string name,
            string description,
            TimeInterval availability,
            List<string> savory,
            List<string> sweet,
            Guid? id = null
        );

        ErrorOr<Breakfast> From(CreateBreakfastRequest request);

        ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request);
    }
}
