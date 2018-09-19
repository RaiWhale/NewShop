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
        public string FullName { get; set; }
        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }
        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}