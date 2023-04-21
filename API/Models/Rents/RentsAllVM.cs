using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Rents
{
    public class RentsAllVM
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public byte State { get; set; }
    }
}