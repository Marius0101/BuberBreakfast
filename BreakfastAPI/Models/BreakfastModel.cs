
using BreakfastAPI.Contracts.Breakfast;
using BreakfastAPI.Contracts.Common;
using BreakfastAPI.Services.Errors;
using ErrorOr;

namespace BreakfastAPI.Models
{
    public class Breakfast
    {
        #region Variable

        public Guid Id { get; set; }
        public string Name {get; set; }
        public string Description {get; set; }
        public TimeInterval Availability {get; set;}
        public List<string> Savory {get; set; }
        public List<string> Sweet  {get; set; }

        #endregion

        #region  Const
        public const int minNameLenght = 3;
        public const int maxNameLenght = 50;

        public const int minDescriptionLenght = 50;
        public const int maxDescriptionLenght = 200;
        #endregion
        public Breakfast(
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

        
    }

}