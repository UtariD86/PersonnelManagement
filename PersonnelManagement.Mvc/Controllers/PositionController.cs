using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Services.Concrete;
using System.Dynamic;
using zurafworks.Shared.Utilities.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class PositionController : Controller
    {
        //PositionManager pm;
        //DepartmentManager dm;
        private readonly IPositionService pm;
        private readonly IDepartmentService dm;
        public PositionController(IPositionService _pm, IDepartmentService _dm)
        {
            //IPositionRepository positionRepository = new EfPositionRepository(new PersonnelManagerContext());
            //IDepartmentRepository departmentRepository = new EfDepatmentRepository(new PersonnelManagerContext());
            //pm = new PositionManager(positionRepository, departmentRepository);
            //dm = new DepartmentManager(departmentRepository);
            pm= _pm;
            dm= _dm;
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
            var data = pm.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult AddPositions(AddPositionModel posModel)
        {
            var newPos = new PositionDetailsDto();


            newPos.PositionName = posModel.NewPosition;
            newPos.DepartmentName = posModel.SelectedDepartment;

            pm.Add(newPos);

            return RedirectToAction("Index");
        }

        public IActionResult DeletePositions(DeletePositionModel posModel)
        {
            pm.Delete(posModel.PositionId, posModel.ModifiedByName);

            return RedirectToAction("Index");
        }

        public IActionResult UpdatePositions(UpdatePositionModel posModel)
        {
            var newPos = new PositionDetailsDto();

            newPos.PositionId = posModel.PositionId;
            newPos.PositionName = posModel.NewPosition;
            newPos.DepartmentName = posModel.SelectedDepartment;
            newPos.ModifiedByName = posModel.ModifiedByName;

            pm.Update(newPos);

            return RedirectToAction("Index");
        }
    }
}
