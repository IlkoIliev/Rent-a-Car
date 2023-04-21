using API.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models.Users
{
    public class UsersEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName("First name")]
        public string Firstname { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string Sirname { get; set; }

        public int RoleId { get; set; }

        [Required]
        public List<RolesPair> Roles { get; set; }
    }
}