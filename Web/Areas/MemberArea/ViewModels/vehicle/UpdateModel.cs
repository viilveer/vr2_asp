using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Domain;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.vehicle
{
    // TODO :: add better validation to model, make, year with carquery API http://www.carqueryapi.com/
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
            vehicle.Make = Make;
            vehicle.UpdatedAt = DateTime.Now;
            vehicle.UpdatedBy = updater.Email;
            return vehicle;
        }
    }

    // TODO :: finish and move attribute validation to separate class to provide validation to multiple models
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class VehicleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty("Model");
            object propertyValue = property.GetValue(instance);



            return new ValidationResult("Valitud sõiduk on väärate andmetega");
        }
    }

    public static class UpdateModelFactory
    {
        public static UpdateModel CreateFromVehicle(Vehicle vehicle)
        {
            return new UpdateModel() {Kw = vehicle.Kw, Engine = vehicle.Engine, Year = vehicle.Year, Model = vehicle.Model, Make = vehicle.Make};
        } 
    }
}