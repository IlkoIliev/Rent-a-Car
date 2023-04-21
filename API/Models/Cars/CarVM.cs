using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Cars
{
    public class CarVM
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public short Year { get; set; }

        public byte Seats { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}