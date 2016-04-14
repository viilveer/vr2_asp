using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Identity;
using Web.Areas.MemberArea.Components.Validators;

namespace Web.Areas.MemberArea.ViewModels.vehicle
{
    public class UpdateModel
    {
        [Required]
        [Vehicle]
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

        public Vehicle UpdateVehicle(Vehicle vehicle, UserInt updater)
        {
            vehicle.Engine = Engine;
            vehicle.Kw = Kw;
            vehicle.Year = Year;
            vehicle.Model = Model;
            vehicle.Make = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Make.Replace("-", " "));
            vehicle.UpdatedAt = DateTime.Now;
            vehicle.UpdatedBy = updater.Email;
            return vehicle;
        }
    }

    public static class UpdateModelFactory
    {
        public static UpdateModel CreateFromVehicle(Vehicle vehicle)
        {
            return new UpdateModel() {Kw = vehicle.Kw, Engine = vehicle.Engine, Year = vehicle.Year, Model = vehicle.Model, Make = vehicle.Make.ToLower().Replace(" ", "-") };
        } 
    }
}