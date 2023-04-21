using DATA.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Rents
{
    public class RentsEditVM : RentsAllVM
    {
        public StateEnum States { get; set; }
    }
}