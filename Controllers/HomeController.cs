using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;

namespace AnReshWebApp.Controllers
{
    public class HomeController : Controller
    {
        private Description description = new Description();


        public ActionResult Index(string newDescription,string readWriteButton)
        {
            switch (readWriteButton)
            {
                case "readButton":
                    return ReadAction();
                case "writeButton":
                    return WriteAction(newDescription);
                default:
                    {
                        ViewData["Description"] = description.DescriptionTextGet();
                        return View();
                    }

                    
            }
            
        }

        public ActionResult ReadAction()
        {
            ViewData["Description"] = description.DescriptionTextGet();
            return View();
        }

        public ActionResult WriteAction(string newDescription)
        {
            ViewData["Description"] = description.DescriptionTextSet(newDescription);
            return View();
        }
    }
}