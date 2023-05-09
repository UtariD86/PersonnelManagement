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
    public class ScheduleController : Controller
    {
        //ShiftTypeManager stm;
        //ScheduleShiftManager ssm;

        private readonly IShiftTypeService stm;
        private readonly IScheduleShiftService ssm;
        public ScheduleController(IScheduleShiftService _ssm, IShiftTypeService _stm)
        {
            //PersonnelManagerContext context = new PersonnelManagerContext();
            //IShiftTypeRepository shiftTypeRepository = new EfShiftTypeRepository(context);
            //IShiftRepository shiftRepository = new EfShiftRepository(context);
            //IEmployeeRepository employeeRepository = new EfEmployeeRepository(context);
            //IScheduleShiftRepository scheduleShiftRepository = new EfScheduleShiftRepository(context);
            //stm = new ShiftTypeManager(shiftTypeRepository);
            //ssm = new ScheduleShiftManager(scheduleShiftRepository, shiftRepository, employeeRepository, shiftTypeRepository);
            ssm = _ssm;
            stm = _stm;
        }
        public IActionResult Index()
        {
            var result = stm.GetAll().Result;
            
            if (result.ResultStatus == ResultStatus.Success)
            {
                dynamic mymodel = new ExpandoObject();
                mymodel.shiftTypes = stm.GetAll().Result.Data;
                return View(mymodel);

            }
            return NotFound();
        }

        public JsonResult GetShiftTypes()
        {
            var data = stm.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public JsonResult GetCalendarDatas()
        {
            var data = ssm.GetAll().Result.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult AddShiftType(AddShiftTypeModel stModel)
        {
            var newSt = new ShiftTypeDetailsDto();


            newSt.ShiftTypeName = stModel.InputtedShiftType;
            newSt.StartTime = stModel.StartTime;
            newSt.EndTime = stModel.EndTime;
            newSt.Color= stModel.Color;
            newSt.ModifiedByName = "mvcSend";


            stm.Add(newSt);

            return RedirectToAction("Index");
        }

        public IActionResult AddScheduleShift(AddScheduleShiftModel ssModel)
        {
            var newSs = new ScheduleShiftDetailsDto();
            newSs.EmployeeId = ssModel.EmployeeId;
            newSs.StartDate = ssModel.StartDate;
            newSs.EndDate = ssModel.EndDate;
            newSs.IsSpecial = ssModel.IsSpecial;
            if(ssModel.IsSpecial == true)
            {
                newSs.SpecialShiftType = ssModel.SpecialShiftType;
                newSs.SpecialStartTime = ssModel.SpecialStartTime;
                newSs.SpecialEndTime = ssModel.SpecialEndTime;
                newSs.SpecialColor = ssModel.SpecialColor;
            }
            else
            {
                newSs.ShiftTypeId = ssModel.ShiftTypeId;
            }
            newSs.ModifiedByName = "mvcSend";


            var entity = ssm.Add(newSs);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteScheduleShift(UpdateScheduleShiftModel ssModel)
        {
            var newSs = new ScheduleShiftDetailsDto();

            newSs.ShiftTypeId = ssModel.ScheduleShiftId;
            newSs.ModifiedByName = "mvcSend";


            ssm.Delete(newSs.ShiftTypeId, newSs.ModifiedByName);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateShiftType(UpdateShiftTypeModel stModel)
        {
            var newSt = new ShiftTypeDetailsDto();

            newSt.ShiftTypeId= stModel.ShiftTypeId;
            newSt.ShiftTypeName = stModel.InputtedShiftType;
            newSt.StartTime = stModel.StartTime;
            newSt.EndTime = stModel.EndTime;
            newSt.Color = stModel.Color;
            newSt.ModifiedByName = "mvcSend";


            stm.Update(newSt);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteShiftType(UpdateShiftTypeModel stModel)
        {
            var newSt = new ShiftTypeDetailsDto();

            newSt.ShiftTypeId = stModel.ShiftTypeId;
            newSt.ModifiedByName = "mvcSend";


            stm.Delete(newSt.ShiftTypeId, newSt.ModifiedByName);

            return RedirectToAction("Index");
        }
    }
}
