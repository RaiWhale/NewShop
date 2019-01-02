using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using TechnologyShop.Models.ViewModel;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class InputsController : Controller
    {
        private NewShopEntities db = new NewShopEntities();

        // GET: Admin/Inputs
        public ActionResult Index()
        {
            int user_id = int.Parse(User.Identity.Name);
            var user = db.Users.Find(user_id);

            IQueryable<Input> inputs = db.Inputs;

            //if user seller
            if(user.UserLevelId == 3)
            {
                inputs = db.Inputs.Where(x => x.UserId == user_id);
            }

            inputs = inputs.Where(x => x.Status > InputStatus.Pending).Include(i => i.Supplier).Include(i => i.User);

            return PartialView(inputs.ToList());
        }

        // GET: Admin/Inputs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Input input = db.Inputs.Find(id);
            if (input == null)
            {
                return HttpNotFound();
            }
            return PartialView(input);
        }


        public ActionResult LoadProducts()
        {
            var products = from p in db.Products select (p);
            ViewBag.products = products.ToList();
            return PartialView(db.Products.ToList());
        }
        // GET: Admin/Inputs/Create
        public ActionResult Create()
        {
            Input input;
            //input.InputCode = MySecurity.RandomString(6);
            //input code = real code on invoice
            //if current user no input status = false -> create new input 
            //else input status is not finish = load for editing
            //get current user id
            int user_id = int.Parse(User.Identity.Name);
            input = db.Inputs.Where(x => x.UserId == user_id && x.Status == 0).SingleOrDefault();
            if(input == null)
            {
                //create new
                input = new Input()
                {
                    UserId = user_id,
                    InputDate = DateTime.Now, //for report
                    SupplierId = db.Suppliers.First().Id,
                    Discount = 0,
                    Tax = 0,
                    Status = InputStatus.Pending
                };
                db.Inputs.Add(input);

                db.SaveChanges();
                //create trigger -> created date
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName");
            return PartialView("InputView", input);
        }


        // GET: Admin/Inputs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int user_id = int.Parse(User.Identity.Name);
            Input input = db.Inputs.Where(x => x.UserId == user_id && x.Id == id).SingleOrDefault();
            if (input == null)
            {
                return PartialView("InputError");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", input.SupplierId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "LoginName", input.UserId);
            return PartialView("InputView", input); //common view
        }


        // POST: Admin/Inputs/InputView
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputView([Bind(Include = "Id,InputCode,InputDate,SupplierId,Tax,Discount")] Input data)
        {
            //update data
            int user_id = int.Parse(User.Identity.Name);
            Input input = db.Inputs.Where(x => x.UserId == user_id && x.Id == data.Id).SingleOrDefault();
            if (input != null)
            {
                input.InputCode = data.InputCode;
                input.InputDate = data.InputDate;
                input.SupplierId = data.SupplierId;
                input.Tax = data.Tax;
                input.Discount = data.Discount;
                if (input.Status == InputStatus.Pending)
                {
                    input.Status = InputStatus.Finished;
                }
                db.SaveChanges();
                return Content("OK");
            }
            else
            {
                return Content("Error!");
            }
        }


        //for input details

        public ActionResult InputDetails(int id)
        {
            var details = db.InputDetails.Where(x => x.InputId == id).ToList();
            details.Add(new InputDetail()
            {
                InputId = id
            });

            string products = " ";
            foreach (var p in db.Products)
            {
                products += "'" + p.Id + "|" + p.Unit + "|" + p.InputPrice + "':'" + p.Id + " → " + p.ProductName + "',";
                //products += "'" + p.Id + "|" + p.Unit + "|" + p.InputPrice + "':'" + p.BarCode + " → " + p.ProductName + "',";
            }

            ViewBag.products = products.Substring(0, products.Length - 1);
            return PartialView(details);
        }


        [HttpPost]
        public ActionResult PostInputDetails(InputDetail detail)
        {
            if(detail.Id == 0)
            {
                //insert into InputDetails
            }
            else
            {
                //update
            }

            return Content("OK|" + detail.Id);
        }

        //get products ext
        public ActionResult SupplierProducts(int id)
        {
            //id == SupplierId
            if (id != 0)
            {
                return PartialView();
            }
            else
            {
                return Content("");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
