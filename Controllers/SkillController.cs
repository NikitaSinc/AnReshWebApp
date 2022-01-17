using AnReshWebApp.Models;
using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Repositories;
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
        public GenericRepository<BaseFilterEntity, Skills> Repository;

        public SkillController(GenericRepository<BaseFilterEntity, Skills> repository)
        {
            Repository = repository;
        }

        public async Task<JsonResult> SendData()
        {
            var skillsList = await Repository.GetAllAsync();

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
                await Repository.DeleteAsync(id);
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
                await Repository.UpdateAsync(skill);
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
               id = await Repository.AddAsync(skill);
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
