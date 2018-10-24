using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnologyShop.Helpers;

namespace TechnologyShop.Areas.Admin.Models
{
    public class FilesViewModel
    {
        public int ProductId;
        public ViewDataUploadFilesResult[] Files { get; set; }
    }
}