using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Controllers
{
    public class CustomerController : Controller
    {
        NewShopEntities db = new NewShopEntities();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM data)
        {

            //Code đăng nhập   
                var acc = db.Users.Where(x => x.LoginName.Equals(data.LoginName)).SingleOrDefault(); //LINQ
                if (acc != null)
                {
                    if (acc.Password.Equals(MySecurity.EncryptPass(data.Password)))
                    {

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


            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM data, HttpPostedFileBase pic)
        {
            //Code đăng ký
            var acc = AutoMapper.Mapper.Map<User>(data);
            try
            {
                string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('\\').Last();

                acc.Password = MySecurity.EncryptPass(acc.Password);
                acc.Avatar = filename;
                acc.CreatedDate = DateTime.Now;
                acc.IsActive = false;
                db.Users.Add(acc);
                db.SaveChanges();

                string path = Server.MapPath("~/Uploads/Avatars") + "\\" + acc.Id;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                pic.SaveAs(path + "\\" + filename);

                return View("RegisterSuccess");
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string Email)
        {
            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}