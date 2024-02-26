
using BreakfastAPI.Contracts.Common;

namespace BreakfastAPI.Contracts.Breakfast
{
    public record UpsertBreakfastRequest(
        string Name,
        string Description,
        TimeInterval Availability,
        List<string> Savory,
        List<string> Sweet
    );
}