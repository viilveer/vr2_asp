using System.ComponentModel.DataAnnotations;
using Domain.Identity;
using Web.Areas.MemberArea.Components.Validators;

namespace Web.Areas.MemberArea.ViewModels.Vehicle
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
            Domain.Vehicle vehicle = new Domain.Vehicle();
            vehicle.UserId = creator.Id;
            vehicle.Engine = Engine;
            vehicle.Kw = Kw;
            vehicle.Year = Year;
            vehicle.Model = Model;
            vehicle.Make = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Make.Replace("-", " "));

            return vehicle;
        }
    }
}