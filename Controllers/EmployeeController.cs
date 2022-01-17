using AnReshWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AnReshWebApp.Filters;
using System.Web.Mvc;
using AnReshWebApp.Services;
using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Repositories;

namespace AnReshWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public GenericRepository<EmployeeFilterModel,Employee> EmployeeRepository;
        public GenericRepository<DepartmentFilterModel, Department> DepartmentRepository;

        public EmployeeController(GenericRepository<EmployeeFilterModel, Employee> employeeRepository, GenericRepository<DepartmentFilterModel, Department> departmentRepository)
        {
            EmployeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
        }

        [HttpPost]
        public async Task<JsonResult> SendData(EmployeeFilterModel employeeFilterModel, int currentPage = 1, int rowPerPage = 10)
        {
            EmployeeRepository.Filter = new EmployeeFilter(employeeFilterModel);
            EmployeeRepository.Paginator = new Paginator();
            EmployeeRepository.Paginator.SetPageSize(currentPage, rowPerPage);

            var employeeList = await EmployeeRepository.GetAllAsyncFiltred();
            return Json(new { employeeList , EmployeeRepository.Paginator}, JsonRequestBehavior.AllowGet) ;
        }

        public ActionResult EmployeeForm()
        {
            return View("_Layout");
        }
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await EmployeeRepository.DeleteAsync(id);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet);
            }

            return Json(JsonRequestBehavior.AllowGet);
        }
    
        public async Task<JsonResult> Edit(Employee employee)
        {
            try
            {
                await EmployeeRepository.UpdateAsync(employee);
            }
            catch (Exception exeption)
            {
                new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
                return Json(exeption, JsonRequestBehavior.AllowGet); 
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Create(Employee employee)
        {
            int id;
            try
            {
               id = await EmployeeRepository.AddAsync(employee);
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