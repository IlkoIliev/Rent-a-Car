using API.Models.Home;
using API.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models.Users
{
    public class UsersCreateVM : Register
    {
        [Required]
        public List<RolesPair> Roles { get; set; }
    }
}