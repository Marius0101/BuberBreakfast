using BreakfastAPI.Contracts.Common;

namespace BreakfastAPI.Contracts.Breakfast
{
    public record BreakfastResponse(
        Guid Id,
        string Name,
        string Description,
        TimeInterval Availability,
        List<string> Savory,
        List<string> Sweet
    );

}