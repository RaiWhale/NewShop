using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Areas.Admin.Models.ViewModel
{
    public class LoginU
    {
        [Key]
        [Required]
        public string LoginName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}