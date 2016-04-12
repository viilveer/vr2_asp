using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;

namespace DAL
{
    //    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            var pwdHasher = new PasswordHasher();

            // we have three roles in our app
            // Admin - can manage blog, users etc
            // User - regular site user
            // CarOwner - can create blogs, etc
            foreach (var role in new List<string> { "Admin", "User", "CarOwner" })
            {
                // Roles
                context.RolesInt.Add(new RoleInt()
                {
                    Name = role,
                });
            }
            
        

            base.Seed(context);
        }
    }
}