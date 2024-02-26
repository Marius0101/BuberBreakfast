
using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Services.Errors;
using ErrorOr;

namespace BreakfastAPI.Models
{
    public class Breakfast
    {
        #region Variable

        public Guid Id {get;}
        public string Name {get;}
        public string Description {get;}
        public TimeInterval Availability {get;}
        public List<string> Savory {get;}
        public List<string> Sweet  {get;}
        
        #endregion

        #region  Const
        public const int minNameLenght = 3;
        public const int maxNameLenght = 50;

        public const int minDescriptionLenght = 50;
        public const int maxDescriptionLenght = 200;
        #endregion

        private Breakfast(
            Guid id,
            string name,
            string description,
            TimeInterval availability,
            List<string> savory,
            List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            Availability= availability;
            Savory = savory;
            Sweet = sweet;
        }

        public static ErrorOr<Breakfast> Create(
            string name,
            string description,
            TimeInterval availability,
            List<string> savory,
            List<string> sweet,
            Guid? id = null

        )
        {
            List<Error> errors= [];

            if(name.Length < minNameLenght || name.Length > maxNameLenght)
            {
                errors.Add(Errors.Breakfast.InvalidName);
            }

            if(description.Length < minDescriptionLenght || description.Length > maxDescriptionLenght)
            {
                errors.Add(Errors.Breakfast.InvalidDescription);
            }

            if (errors.Count>0){
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

        public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                availability: request.Availability,
                request.Savory,
                request.Sweet
            );
        }

        public static ErrorOr<Breakfast> From(Guid id,UpsertBreakfastRequest request)
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