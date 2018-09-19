using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class ForgetPasswordVM
    {
        [Key]

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone]
        public string Phone { get; set; }
    }
}