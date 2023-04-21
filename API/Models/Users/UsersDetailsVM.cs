using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Users
{
    public class UsersDetailsVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Sirname { get; set; }
        public string Egn { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}