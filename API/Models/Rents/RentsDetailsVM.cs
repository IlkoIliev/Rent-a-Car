using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Rents
{
    public class RentsDetailsVM
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Sirname { get; set; }

        public string Egn { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public short Year { get; set; }

        public byte Seats { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public byte State { get; set; }
    }
}