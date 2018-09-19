using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        // GET: Admin/Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string LoginName, string Password)
        {
            try
            {
                var acc = db.Users.Where(x=>x.LoginName.Equals(LoginName)).SingleOrDefault();
                if (acc != null)
                {
                    if (acc.Password.Equals(MySecurity.EncryptPass(Password)))
                    {
                        Response.Cookies["UserName"].Value = acc.LoginName.ToUpper();

                        FormsAuthentication.SetAuthCookie(acc.Id.ToString(), false);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.Message = "Password wrong!";
                    }
                }
                else
                {
                    ViewBag.Message = "Login Name not exist!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }


    }
}