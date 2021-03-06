﻿using System.ComponentModel.DataAnnotations;
using BLL.Validators;

namespace BLL.ViewModels.Vehicle
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

        public Domain.Vehicle UpdateVehicle(Domain.Vehicle vehicle)
        {
            vehicle.Engine = Engine;
            vehicle.Kw = Kw;
            vehicle.Year = Year;
            vehicle.Model = Model;
            vehicle.Make = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Make.Replace("-", " "));
            return vehicle;
        }
    }

    public static class UpdateModelFactory
    {
        public static UpdateModel CreateFromDetailsModel(DetailsModel vehicle)
        {
            return new UpdateModel() {Kw = vehicle.Kw, Engine = vehicle.Engine, Year = vehicle.Year, Model = vehicle.Model, Make = vehicle.Make.ToLower().Replace(" ", "-") };
        } 
    }
}