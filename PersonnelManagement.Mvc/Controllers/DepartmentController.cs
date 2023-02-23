﻿using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Services.Concrete;
using System.Dynamic;
using zurafworks.Shared.Utilities.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager dm;
        public DepartmentController()
        {
            IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            dm = new DepartmentManager(departmentRepository);

        }


        public IActionResult Index()
        {

            var result = dm.GetAll().Result;
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();
                mymodel.Departments = dm.GetAll().Result.Data;

                return View(mymodel);

            }
            return NotFound();
        }

        public JsonResult GetDepartments()
        {
            var data = dm.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult AddDepartment(AddDepartmentModel depModel)
        {
            var newDep = new DepartmentDetailsDto();


            newDep.DepartmentName = depModel.NewDepartment;

            dm.Add(newDep);

            return RedirectToAction("Index");
        }

        //public IActionResult DeleteEmployees(DeleteEmployeeModel empModel)
        //{
        //    pm.Delete(empModel.EmployeeId, empModel.ModifiedByName);

        //    return RedirectToAction("Index");
        //}
    }
}

