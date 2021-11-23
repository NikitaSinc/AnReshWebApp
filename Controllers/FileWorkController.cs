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
            return Json(description.DescriptionTextGet(), JsonRequestBehavior.AllowGet);
        }

        public HttpStatusCodeResult SetFile(string data)
        {
            try
            {
                description.DescriptionTextSet(data);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}