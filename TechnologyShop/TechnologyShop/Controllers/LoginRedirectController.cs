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
        public ActionResult Index(string returnURL)
        {
            string[] d = returnURL.Split('/');
            switch (d[1])
            {
                case "Admin": return Redirect("~/Admin/Account/Login");
                    //break;

                case "Customer": return Redirect("~/Customer/Login");
                    //break;
            }
            return HttpNotFound();


        }
    }
}