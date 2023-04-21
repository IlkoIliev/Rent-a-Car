using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Rents
{
    public class RentsCreateVM : RentsVM
    {
        public string Firstname { get; set; }

        public string Sirname { get; set; }

        public Car Car { get; set; }
    }
}