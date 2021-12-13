using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;
using AnReshWebApp.Filters;

namespace AnReshWebApp.Controllers
{

    public class DepartmentController : Controller
    {

        public DepartmentRepository repository = new DepartmentRepository();
        [HttpGet]
        public async Task<JsonResult> SendData()
        {
            var departmentsList = await repository.GetAllAsync();
            return Json(departmentsList,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SendData(Department department)
        {
            DepartmentFilter filter = new DepartmentFilter();
            filter.SetFilter(department);

            var departmentsList = await repository.GetAllAsyncFiltered(filter);
            return Json(departmentsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepartmentForm()
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

        public async Task<JsonResult> Edit(Department department)
        {
            try
            {
                await repository.UpdateAsync(department);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet);
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Department department)
        {
            int id;
            try
            {
                id = await repository.AddAsync(department);
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