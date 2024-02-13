using System.Runtime.CompilerServices;

namespace BuberBreakfast.Contracts.Breakfast;

public record struct UpsertBreakfastResponse
(
    bool isNewlyCreated
);