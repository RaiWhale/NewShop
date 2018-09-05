using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Controllers
{
    public class CustomerController : Controller
    {
        NewShopEntities db = new NewShopEntities();

        //chi can post thoi, chu form o layout roi; ok sep tiep di
        [HttpPost]
        public ActionResult Login(string LoginName, string Password)
        {
            string ms = "";
            var login = db.Users.Where(x => x.LoginName.Equals(LoginName)).SingleOrDefault();
            if (login != null)
            {
                if (login.Password.Equals(MySecurity.EncryptPass(Password)))
                {
                    //roi chua ok
                    //de sau di

                    ms = "OK";
                }
                else
                {
                    ms = "...";
                }
            }
            else
            {
                ms = "...";
            }
            
            return Content(ms);
        }


        [HttpPost]

        public ActionResult Register(RegisterVM data)
        {
            ViewBag.RegisterMS = "bay doi!";

            return View("Connect");
        }

    }
}