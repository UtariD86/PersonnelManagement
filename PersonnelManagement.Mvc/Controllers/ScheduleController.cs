using Microsoft.AspNetCore.Mvc;
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
    public class ScheduleController : Controller
    {
        ShiftTypeManager stm;

        public ScheduleController()
        {
            IShiftTypeRepository shiftTypeRepository = new EfShiftTypeRepository(new PersonnelManagerContext());
            stm = new ShiftTypeManager(shiftTypeRepository);
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
    }
}
