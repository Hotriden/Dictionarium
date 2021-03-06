using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionarium.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Jwt { get; set; }
    }
}
