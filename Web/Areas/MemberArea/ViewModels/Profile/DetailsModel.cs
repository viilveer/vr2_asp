using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.Profile
{
    public class DetailsModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime MemberSinceDateTime { get; set; }
    }

    public static class DetailsModelFactory
    {
        public static DetailsModel CreateFromUserInt(UserInt user)
        {
            return new DetailsModel()
            {
                Email = user.Email,
                UserName = user.FirstLastName, // TODO :: this is empty, fix when creating a better registration
                MemberSinceDateTime = user.CreatedAt,
            };
        }
    }
}