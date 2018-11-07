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
                var email = db.Customers.Where(x => x.Email.Equals(data.Email)).SingleOrDefault(); //LINQ
                if (email != null)
                {
                    if (email.Password.Equals(MySecurity.EncryptPass(data.Password)))
                    {

                    FormsAuthentication.SetAuthCookie(email.Id.ToString(), false);
                    return RedirectToAction("Index","Home");
                        
                    }
                    else
                    {
                        ViewBag.Message = "Password wrong!";
                    }
                }
                else
                {
                    ViewBag.Message = "Email not exist!";
                }


            
            return View();
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UpdateProfile");
            };
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM data, HttpPostedFileBase pic)
        {
            //Code đăng ký
            var acc = AutoMapper.Mapper.Map<Customer>(data);
            try
            {

                string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();
                acc.Avatar = filename;
                acc.CreatedDate = DateTime.Now;
                acc.IsActive = false;

                db.Customers.Add(acc);
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
        public ActionResult ForgotPassword(ForgetPasswordVM data, string NewPassword, string Email)
        {
            var email = db.Customers.Where(x => x.Email.Equals(data.Email)).SingleOrDefault();


            if (email != null)
            {
                if ((email.Phone.Equals(data.Phone)) && email.CustomerName.Equals(data.CustomerName))
                {
                    NewPassword = RandomString2(6);
                    email.Password = NewPassword;
                    db.SaveChanges();
                    ViewBag.Message = email.Password;
                    return RedirectToAction("Login", "Customer");
                }
                else
                {
                    ViewBag.Message = "Phone or Customer name Invalid!";
                }
            }
            else
            {
                ViewBag.Message = "Email invalid!";
            }


            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UpdateProfile()
        {
            var email = db.Customers.Find(int.Parse(User.Identity.Name));
            var data = AutoMapper.Mapper.Map<UpdateProfileVM>(email);
            return View(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProfile(UpdateProfileVM data, HttpPostedFileBase pic)
        {
    
            try
            {
                string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();
                var email = db.Customers.Find(int.Parse(User.Identity.Name));
                data.Id = email.Id;
                data.Avatar = filename;

                AutoMapper.Mapper.Map(data, email);
               

                db.SaveChanges();

                string path = Server.MapPath("~/Uploads/Avatars") + "\\" + data.Id;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                pic.SaveAs(path + "\\" + filename);

                ViewBag.Message = "Update Successfully";
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
            return View();
        }

        static Random rnd = new Random();
        public string RandomString2(int length)
        {
            const string chars = "abcdef0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}