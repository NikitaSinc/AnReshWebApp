using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;

namespace AnReshWebApp.Controllers
{
    public class FileWorkController : Controller
    {
        private Description description = new Description();

        public ActionResult FileWorkPage()
        {
            return View("_Layout");
        }

        public JsonResult GetFile()
        {
            //для чего ViewData?
            return Json(ViewData["Description"] = description.DescriptionTextGet(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetFile(string data)
        {
            description.DescriptionTextSet(data);
            return RedirectToAction("FileWorkPage");
        }
    }
}