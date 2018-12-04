using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class RegisterVM
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public Gender Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }


    }
}