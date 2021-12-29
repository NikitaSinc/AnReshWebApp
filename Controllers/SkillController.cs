using AnReshWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AnReshWebApp.Controllers
{
    public class SkillController : Controller
    {
        public SkillsRepository repository = new SkillsRepository();
        public async Task<JsonResult> SendData()
        {
            var skillsList = await repository.GetAllAsync();

            return Json(skillsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SkillForm()
        {
            return View("_Layout");
        }

        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet);
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Edit(Skills skill)
        {
            try
            {
                await repository.UpdateAsync(skill);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet);
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Skills skill)
        {
            int id;
            try
            {
               id = await repository.AddAsync(skill);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet);
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}
