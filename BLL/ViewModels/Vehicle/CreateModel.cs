using System.ComponentModel.DataAnnotations;
using BLL.Validators;
using Domain.Identity;

namespace BLL.ViewModels.Vehicle
{
    public class CreateModel
    {
        [Vehicle]
        [Required]
        [MaxLength(64)]
        public string Make { get; set; }

        [Required]
        [MaxLength(64)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Kw { get; set; }

        [MaxLength(64)]
        [Required]
        public string Engine { get; set; }

        public Domain.Vehicle GetVehicle(UserInt creator)
        {
            Domain.Vehicle vehicle = new Domain.Vehicle
            {
                UserId = creator.Id,
                Engine = Engine,
                Kw = Kw,
                Year = Year,
                Model = Model,
                Make = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Make.Replace("-", " "))
            };

            return vehicle;
        }
    }
}