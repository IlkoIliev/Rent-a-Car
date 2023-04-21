using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Initializers
{
    public static class Seeder
    {
        public static void Seed(RentACarDbContext context)
        {
            context.Roles.Add(new Role()
            {
                Name = "owner"
            });

            context.Roles.Add(new Role()
            {
                Name = "buyer"
            });

            context.Users.Add(new User
            {
                Firstname = "Owner",
                Sirname = "Owner",
                Password = "owner",
                Username = "owner",
                Egn = "0000000",
                PhoneNumber = "000000",
                Email = "Example@mail.anything",
                RoleId = 1
            });

            context.Users.Add(new User()
            {
                Firstname = "Buyer",
                Sirname = "Buyer",
                Username = "buyer",
                Password = "buyer",
                Egn = "10000000",
                PhoneNumber = "1000000",
                Email = "example@mail.anything",
                RoleId = 2
            });

            context.Cars.Add(new Car()
            {
                Brand = "volkswagen",
                Model = "Passat",
                Year = 2010,
                Seats = 5,
                Description = "Super car",
                Price = 50,
            });

            context.SaveChanges();
        }
    }
}
