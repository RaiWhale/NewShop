using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnologyShop.Areas.Admin.Models.ViewModel;
using TechnologyShop.Models;

namespace TechnologyShop.Areas.Admin.Controllers
{
    public class AccountsController : Controller
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
                        return RedirectToAction("Index","Dashboard");
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUVM data, HttpPostedFileBase pic)
        {
            var acc = AutoMapper.Mapper.Map<User>(data);
            try
            {
                string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();

                acc.Password = MySecurity.EncryptPass(data.Password);
                acc.Avatar = filename;
                acc.CreatedDate = DateTime.Now;
                acc.IsActive = false;
                db.Users.Add(acc);
                db.SaveChanges();

                string path = Server.MapPath("~/Uploads/Logo") + "\\" + acc.Id;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                pic.SaveAs(path + "\\" + filename);

                return View("RegisterSuccess","Customer");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}