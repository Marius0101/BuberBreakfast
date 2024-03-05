using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Models;
using ErrorOr;

namespace BreakfastAPI.Services.Breakfasts
{
    public class BreakfastService : IBreakfastService
    {
        public ErrorOr<Breakfast> Create(
            string name,
            string description,
            TimeInterval availability,
            List<string> savory,
            List<string> sweet,
            Guid? id = null

        )
        {
            List<Error> errors = [];

            if (name.Length < Breakfast.minNameLenght || name.Length > Breakfast.maxNameLenght)
            {
                errors.Add(Errors.BreakfastErrors.InvalidName);
            }

            if (description.Length < Breakfast.minDescriptionLenght || description.Length > Breakfast.maxDescriptionLenght)
            {
                errors.Add(Errors.BreakfastErrors.InvalidDescription);
            }

            if (errors.Count > 0)
            {
                return errors;
            }
            return new Breakfast(

                id ?? Guid.NewGuid(),
                name,
                description,
                availability,
                savory,
                sweet
            );
        }

        public ErrorOr<Breakfast> From(CreateBreakfastRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                availability: request.Availability,
                request.Savory,
                request.Sweet
            );
        }

        public ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                availability: request.Availability,
                request.Savory,
                request.Sweet,
                id
            );
        }
    }
}
