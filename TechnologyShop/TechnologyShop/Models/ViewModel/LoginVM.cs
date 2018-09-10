using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class LoginVM
    {
        [Key]
        public string LoginName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}