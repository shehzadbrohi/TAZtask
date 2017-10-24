using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using System.IO;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {

        ahmedEntities1 db = new ahmedEntities1();

        // GET: Home
        public ActionResult Index()
        {
            TempData["cat"] = db.Categories.ToList();
            return View(db.Images.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images/"), pic);

                string imgtype = Path.GetExtension(file.FileName);


                if (imgtype.Equals(".jpg"))
                {
                    file.SaveAs(path);

                    Image img = new Image();
                    img.Image_Url = pic;
                    db.Images.Add(img);
                    db.SaveChanges();


                    TempData["mxg"] = "Uploaded Succesfully";
                    
                }
                else
                {
                    TempData["mxg"] = "Please upload jpg, png or gif file only.";
                }

                

                return RedirectToAction("Index","Home");

            }
            else
            {
                TempData["mxg"] = "Not Uploaded";
                return RedirectToAction("Index", "Home");
            }
            
        
        }


        //retrive images from database
        public ActionResult AllImages()
        {
            var allpics = db.Images.ToList();
            return PartialView("_allimages",allpics);
        }


    }
}