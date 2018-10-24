using TechnologyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyShop.Models;
using System.IO;
using System.Web.Hosting;
using TechnologyShop.Areas.Admin.Models;

namespace TechnologyShop.Areas.Admin.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
    {
        NewShopEntities db = new NewShopEntities();
        FilesHelper filesHelper;
        String tempPath = "~/Pictures/";
        String serverMapPath = "~/Uploads/Pictures/";

        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Uploads/Pictures/";
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public FileUploadController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }
        // GET: Admin/FileUpload
        public ActionResult Index(int productId)
        {
            string path = StorageRoot + productId.ToString();                     //The path for the uploads
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                ViewData["FileEntryCollection"] = di.GetFiles();
            }


            FilesViewModel filesViewModel = new FilesViewModel
            {
                ProductId = productId
            };
            return View(filesViewModel);
        }

        public ActionResult Show()
        {
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(int productId)
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath, productId.ToString());
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                foreach (var fileUploaded in resultList)
                {
                    Picture productImage = new Picture
                    {
                        ProductId = productId,
                        Url = fileUploaded.thumbnailUrl
                    };
                    db.Pictures.Add(productImage);
                }
                db.SaveChanges();
                return Json(files);
            }
        }
        public JsonResult GetFileList(int productId)
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath, productId.ToString());
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file, int productId)
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath, productId.ToString());
            filesHelper.DeleteFile(file);
            string thumbURL = String.Format("{0}{1}/thumbs/{2}", UrlBase, productId, file);
            Picture productImage = db.Pictures.Where(i => i.ProductId == productId && i.Url.Equals(thumbURL)).FirstOrDefault();
            db.Pictures.Remove(productImage);
            db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}