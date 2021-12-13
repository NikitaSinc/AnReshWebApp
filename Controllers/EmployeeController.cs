﻿using AnReshWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AnReshWebApp.Filters;
using System.Web.Mvc;

namespace AnReshWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeRepository employeeRepository = new EmployeeRepository();
        public DepartmentRepository departmentRepository = new DepartmentRepository();

        [HttpPost]
        public async Task<JsonResult> SendData(Employee employee)
        {
                EmployeeFilter filter = new EmployeeFilter();
                filter.SetFilter(employee);
                var employeeList = await employeeRepository.GetAllAsyncFiltered(filter);
                return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> SendData()
        {
                var employeeList = await employeeRepository.GetAllAsync();
                return Json(employeeList, JsonRequestBehavior.AllowGet);     
        }

        public ActionResult EmployeeForm()
        {
            return View("_Layout");
        }
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await employeeRepository.DeleteAsync(id);
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
                await employeeRepository.UpdateAsync(employee);
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
               id = await employeeRepository.AddAsync(employee);
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