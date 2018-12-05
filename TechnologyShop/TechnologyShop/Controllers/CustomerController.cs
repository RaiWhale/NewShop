using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
                    return RedirectToAction("Index", "Home");

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
            data.Password = MySecurity.EncryptPass(data.Password);
            data.RePassword = MySecurity.EncryptPass(data.RePassword);
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
            var cus = db.Customers.Where(x => x.Email.Equals(data.Email)).SingleOrDefault();


            if (cus != null)
            {
                if ((cus.Phone.Equals(data.Phone)) && cus.Email.Equals(data.Email))
                {
              
                    cus.Token = MySecurity.EncryptPass(DateTime.Now.Ticks + cus.Email + MySecurity.RandomString(6));
                    db.SaveChanges();

                    var url = string.Format("/Customer/NewPassword/?token={0}&cid={1}", cus.Token,cus.Id);
                    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

                    string subject = "Forgot Password !";
                    string body = "<br/><br/>Here is your password! Please log out account to try new password" +
                        "<br/> <a href='" + link + "'><h3>" + link + "</h3>  </a>";

                    if (MySecurity.SendMail(data.Email, subject, body))
                    {
                        return RedirectToAction("Login", "Customer");
                    }
                    else
                    {
                        ViewBag.Message = "Error!";
                    }
                }
                else
                {
                    ViewBag.Message = "Phone or Email Invalid!";
                }
            }
            else
            {
                ViewBag.Message = "Email invalid!";
            }
     

            return View(data);
        }

        public ActionResult NewPassword(int? cid, string token)
        {
            var cus = db.Customers.Where(x => x.Token.Equals(token) && x.Id == cid).SingleOrDefault();
            if(cus == null)
            {
                return View("ErrorResetPassword");
            }
            return View();
        }

        [HttpPost]
        public ActionResult NewPassword(int? cid,string token, string NewPassword, string RePassword)
        {
            var cus = db.Customers.Where(x => x.Token.Equals(token) && x.Id == cid).SingleOrDefault();
            if (cus == null)
            {
                return View("ErrorResetPassword");
            }
            else
            {
                if (!NewPassword.Equals(RePassword))
                {
                    ViewBag.Message = "Please enter the same Password as above";
                }
                else
                {
                    cus.Password = MySecurity.EncryptPass(NewPassword);
                    cus.Token = "";
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
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
            var cus = db.Customers.Find(int.Parse(User.Identity.Name));
            var data = AutoMapper.Mapper.Map<UpdateProfileVM>(cus);
            return View(data);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProfile(UpdateProfileVM data, HttpPostedFileBase pic)
        {

            try
            {
                var cus = db.Customers.Find(int.Parse(User.Identity.Name));
                data.Id = cus.Id;
                data.Avatar = cus.Avatar;

                if (pic!=null)
                {
                    
                    string filename = DateTime.Now.Ticks + "_" + pic.FileName.Split('/').Last();
                    
                    string path = Server.MapPath("~/Uploads/Avatars") + "\\" + data.Id;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    if (System.IO.File.Exists(path + "\\" + data.Avatar))
                    {
                        System.IO.File.Delete(path + "\\" + data.Avatar);

                    }

                    pic.SaveAs(path + "\\" + filename);
                    data.Avatar = filename;

                }


                AutoMapper.Mapper.Map(data, cus);


                db.SaveChanges();



                ViewBag.Message = "Update Successfully";
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
            return View(data);
        }


     
 
    }
}