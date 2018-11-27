using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnologyShop.Areas.Admin.Models.ViewModel
{
    public class UploadFileVM
    {
        [Key]
        public int ProductId { get; set; }
        public string Url { get; set; }
    }
}