﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;
using Domain.Identity;
using Web.Areas.MemberArea.Components.Validators;

namespace Web.Areas.MemberArea.ViewModels.vehicle
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

        public Vehicle GetVehicle(UserInt creator)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.UserId = creator.Id;
            vehicle.Engine = Engine;
            vehicle.Kw = Kw;
            vehicle.Year = Year;
            vehicle.Model = Model;
            vehicle.Make = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Make.Replace("-", " "));
            vehicle.CreatedAt = DateTime.Now;
            vehicle.CreatedBy = creator.Email;

            return vehicle;
        }
    }
}