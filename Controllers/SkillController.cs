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

        public async Task<HttpStatusCodeResult> Delete(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Edit(Skills skill)
        {
            try
            {
                await repository.UpdateAsync(skill);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPost]
        public async Task<HttpStatusCodeResult> Create(Skills skill)
        {
            try
            {
                await repository.AddAsync(skill);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}
