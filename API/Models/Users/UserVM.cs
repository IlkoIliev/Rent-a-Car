using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Users
{
    public class UserVM
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Sirname { get; set; }

        public string RoleName { get; set; }
    }
}