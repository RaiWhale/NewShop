using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class UpdateProfileVM
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public Gender Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}