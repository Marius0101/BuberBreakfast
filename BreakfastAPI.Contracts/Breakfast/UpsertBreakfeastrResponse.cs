using System.Runtime.CompilerServices;

namespace BreakfastAPI.Contracts.Breakfast;

public record struct UpsertBreakfastResponse
(
    bool isNewlyCreated
);