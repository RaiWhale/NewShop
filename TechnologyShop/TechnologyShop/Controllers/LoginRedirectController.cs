using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnologyShop.Controllers
{
    public class LoginRedirectController : Controller
    {
        // GET: LoginRedirect
        public ActionResult Index()
        {
            return Redirect("~/Customer/Login");
        }
    }
}