using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Services.Abstract;
using System.Dynamic;
using zurafworks.Shared.Results.ComplexTypes;
using zurafworks.Shared.Results.Concrete;

namespace PersonnelManagement.Mvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService dm;
        private readonly UserManager<User> _userManager;
        public DepartmentController(IDepartmentService _dm,UserManager<User> userManager)
        {
            dm = _dm;
            _userManager = userManager;
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
        //yetki
        public JsonResult GetDepartments()
        {
            var data = dm.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateDepartment(int? id)
        {
            var model = new DepartmentModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (id.HasValue)
            {
                var result = await dm.GetById(id.Value);
                var entity = result.Data;
                if (entity!=null && result.ResultStatus == ResultStatus.Success)
                {
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.ModifiedByName = user!=null ? user.Name :"";
                }
                TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
            }
            return PartialView(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveDepartment(DepartmentModel model)
        {
            var message = "resultMessage";
            var id = model.Id;
            var newDep = new Department();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            newDep.Name = model.Name;
            newDep.ModifiedByName = user != null ? user.Name : "";
            if (id != 0 && id != null)
            {
                newDep.Id = id;
                var result = dm.Update(newDep);
                if (!string.IsNullOrEmpty(result.Result.Message))
                {
                    message = result.Result.Message;
                }
            }
            if(id == 0)
            {
                newDep.CreatedByName = newDep.ModifiedByName;
                var result = dm.Add(newDep);
                if (!string.IsNullOrEmpty(result.Result.Message))
                {
                    message = result.Result.Message;
                }
            }
            if(id == null || model.Name == null)
            {
                message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
            }
            return Json(message);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartments(DepartmentModel model)
        {
            if(model.Id != null || model.Id != 0)
            {
                var department = new Department();
                department.Id = model.Id;
                var result = await dm.Delete(department);
                if (!string.IsNullOrEmpty(result.Message))
                {
                    TempData["PopupMessage"] = result.Message;
                }
            }
            else
            {
                TempData["PopupMessage"] = "Silinirken bir hata oluştu!";
            }
            return RedirectToAction("Index");
        }
    }
}

