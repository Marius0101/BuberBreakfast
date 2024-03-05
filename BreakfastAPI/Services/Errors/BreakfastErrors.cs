using ErrorOr;

namespace BreakfastAPI.Services.Errors;


    public static class BreakfastErrors
    {
        public static Error InvalidName => Error.Validation(
            code:           "Breakfast.InvalidName",
            description:    $"The breakfast name should be between {Models.Breakfast.minNameLenght} and " +
            $"{Models.Breakfast.maxNameLenght}."
        );

        public static Error InvalidDescription => Error.Validation(
            code:           "Breakfast.InvalidDescription",
            description:    $"The breakfast description should be between {Models.Breakfast.minDescriptionLenght} and " +
            $"{Models.Breakfast.maxDescriptionLenght}."
        );
    
        public static Error NotFound => Error.NotFound(
            code:           "Breakfast.NotFound",
            description:    "The breakfast was not found in the database."
        );

        public static Error ConflictExistingID => Error.Conflict(
            code: "Breakfast.Id.Conflict",
            description: "The guid id is already in database"
        );
        public static Error FailureBreakfastNull => Error.Failure(
                code: "Breakfast.Null",
                description: "Invalid breakfast data: null breakfast"
        );
        public static Error FailureBreakfastIdNull => Error.Failure(
            code: "Breakfast.Id.Null",
            description: "Invalid breakfast data: null id"
        );
}