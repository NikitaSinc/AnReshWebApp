using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;
using AnReshWebApp.Filters;
using AnReshWebApp.Repositories;
using AnReshWebApp.Models.FilterEntity;

namespace AnReshWebApp.Controllers
{
    
    public class DepartmentController : Controller
    {
        private GenericRepository<DepartmentFilterModel, Department> Repository;
       

        public DepartmentController(GenericRepository<DepartmentFilterModel, Department> departmentRepository)
        {
            Repository = departmentRepository;
        }

        public async Task<JsonResult> SendData(DepartmentFilterModel department)
        {
            Repository.Filter = new DepartmentFilter(department);
            var departmentsList = await Repository.GetAllAsyncFiltred();
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
                await Repository.DeleteAsync(id);
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
                await Repository.UpdateAsync(department);
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
                id = await Repository.AddAsync(department);
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