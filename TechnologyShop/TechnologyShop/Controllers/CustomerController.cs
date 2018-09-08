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


        public ActionResult Connect()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string loginName, string password)
        {

               
                var acc = db.Users.Where(x => x.LoginName.Equals(loginName)).SingleOrDefault();
                if (acc != null)
                {
                    if (acc.Password.Equals(MySecurity.EncryptPass(password)))
                    {
                    Response.Cookies["LoginName"].Value = acc.LoginName.ToUpper();
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
            var acc = AutoMapper.Mapper.Map<User>(data);
            try
            {
                string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();

                acc.Password = MySecurity.EncryptPass(acc.Password);
                acc.Avatar = filename;
                acc.CreatedDate = DateTime.Now;
                acc.IsActive = false;
                db.Users.Add(acc);
                db.SaveChanges();

                string path = Server.MapPath("~/Uploads/Logo") + "\\" + acc.Id;
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
    }
}