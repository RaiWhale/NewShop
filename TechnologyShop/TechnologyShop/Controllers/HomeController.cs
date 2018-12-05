using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Controllers
{
    public class HomeController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.products_hot = db.Products.OrderByDescending(x => x.OutputPrice).Take(5).ToList();
            var topic_items = db.Topics.ToList();
            ViewBag.topic_items = topic_items;

            ViewBag.products_old = db.Products.Where(x => x.Discount >= 50).Take(10).ToList();
            //var topic_id = topic_items.Select(x => x.Id).ToList();
            //ViewBag.products_topic = db.Products.Where(x => topic_id.Contains(x.Category.TopicId)).Take(5).ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SimpleMail data)
        {
            if (ModelState.IsValid)
            {
                //Create MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("lehoanghai22@gmail.com");
                mail.To.Add(data.To);
                mail.Subject = data.Subject;
                mail.Body = data.Body;

                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                //Create SMTP for send mail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lehoanghai22", "yeumynhju1");
                smtp.EnableSsl = true;

                //Call Send mail -> Check all Spam
                smtp.Send(mail);

                return RedirectToAction("Success");
            }

            return View(data);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
