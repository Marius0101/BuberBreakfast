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
            code:           "Breakfast.Description",
            description:    $"The breakfast description should be between {Models.Breakfast.minDescriptionLenght} and " +
            $"{Models.Breakfast.maxDescriptionLenght}."
        );
    
        public static Error NotFound => Error.NotFound(
            code:           "Breakfast.NotFound",
            description:    "The breakfast was not found in the database."
        );
    }