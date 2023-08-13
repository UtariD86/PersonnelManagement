using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Services.Concrete;
using System.Data;
using System.Dynamic;
using zurafworks.Shared.Entities.Abstract;
using zurafworks.Shared.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class PositionController : Controller
    {
        private const string ActionName = "Index";
        private readonly IPositionService pm;
        private readonly IDepartmentService dm;
        private readonly UserManager<User> _userManager;
        public PositionController(IPositionService _pm, IDepartmentService _dm, UserManager<User> userManager)
        {
            pm = _pm;
            dm = _dm;
            _userManager = userManager;
        }


        public IActionResult Index()
        {

            var result = pm.GetAll().Result;
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();
                mymodel.Positions = pm.GetAll().Result.Data;
                mymodel.Departments = dm.GetAll().Result.Data;

                return View(mymodel);

            }
            return NotFound();
        }

        public JsonResult GetPositions()
        {
            var lastData = pm.GetAllWithRelations().Result.Data;
            //var data = pm.GetAll().Result.Data;
            //var lastData = new List<PositionModel>();

            //for (int i = 0; i < data.Count; i++)
            //{
            //    var position = new PositionModel();
            //    position.Id = data[i].Id;
            //    position.Name = data[i].Name;
            //    position.DepartmentId = data[i].DepartmentId;
            //    position.DepartmentName = dm.GetById(data[i].DepartmentId).Result.Data.Name;

            //    lastData.Add(position);
            //}

            return Json(lastData, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdatePosition(int? id)
        {
            var model = new PositionModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var departments = dm.GetAll().Result.Data;
            if (id.HasValue)
            {
                var pResult = await pm.GetById(id.Value);
                var pEntity = pResult.Data;
                if (pEntity != null && pResult.ResultStatus == ResultStatus.Success)
                {
                    var dResult = await dm.GetById(pEntity.DepartmentId);
                    var dEntity = dResult.Data;
                    if(dEntity != null && dResult.ResultStatus == ResultStatus.Success)
                    {
                        model = new PositionModel
                        {
                            Id = pEntity.Id,
                            Name = pEntity.Name,
                            DepartmentId = dEntity.Id,
                            DepartmentName = dEntity.Name,
                            ModifiedByName = user != null ? user.Name : ""
                        };
                    }
                }
                TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
            }
            model.Departments = (List<Department>)departments;
            return PartialView(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SavePosition(PositionModel model)
        {
            var message = "resultMessage";
            var id = model.Id;
            var newPos = new Position();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(model.Name!= null && model.Name != "") 
            {
                newPos.Name = model.Name;
                newPos.ModifiedByName = user != null ? user.Name : "";
                newPos.DepartmentId = model.DepartmentId.Value;
                if (id != 0 && id != null)
                {
                    

                    newPos.Id = id;
                    if (model.DepartmentId != null && model.DepartmentId != 0)
                    {
                        newPos.DepartmentId = model.DepartmentId.Value;
                    }
                    var result = pm.Update(newPos);
                    if (!string.IsNullOrEmpty(result.Result.Message))
                    {
                         message = result.Result.Message;
                    }
                }
                if (id == 0)
                {
                    newPos.CreatedByName = newPos.ModifiedByName;
                    var result = pm.Add(newPos);
                    if (!string.IsNullOrEmpty(result.Result.Message))
                    {
                         message = result.Result.Message;
                    }
                }
                if (id == null || model.Name == null)
                {
                     message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
                }
            }
            return Json(message);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePositions(PositionModel model)
        {
            var message = "tempMessage";
            if (model.Id != null || model.Id != 0)
            {
                var position = new Position();
                position.Id = model.Id;
                var result = await pm.Delete(position);
                if (!string.IsNullOrEmpty(result.Message))
                {
                    message = result.Message;
                }
            }
            else
            {
                message = "Silinirken bir hata oluştu!";
            }
            return Json(message);
        }
    }
}
