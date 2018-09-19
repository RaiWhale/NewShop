using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Areas.Admin.Models.ViewModel
{
    public class RegisterUVM
    {
        [Key]
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}