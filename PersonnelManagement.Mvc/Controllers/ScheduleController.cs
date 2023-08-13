using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Data.Concrete.Repositories;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Helpers.Abstract;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Services.Concrete;
using System.Data;
using System.Dynamic;
using zurafworks.Shared.Results.ComplexTypes;

namespace PersonnelManagement.Mvc.Controllers
{
    public class ScheduleController : Controller
    {
        //ShiftTypeManager stm;
        //ScheduleShiftManager ssm;

        private readonly IShiftTypeService stm;
        private readonly IScheduleShiftService ssm;
        private readonly IShiftService sm;
        private readonly UserManager<User> _userManager;
        //private readonly IThemeColorHelper themeColorHelper;
        public ScheduleController(IScheduleShiftService _ssm, IShiftTypeService _stm, IShiftService _sm, UserManager<User> userManager/*, IThemeColorHelper _themeColorHelper*/)
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
            sm = _sm;
            _userManager = userManager;
            //themeColorHelper = _themeColorHelper;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTemplate()
        {
            var stream = stm.CreateExcel();
            string fileName = "shiftTypes.xlsx";
            try
            {
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetTemplate(IFormFile excelStream)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            bool isSuccess = false;
            IList<ShiftType> shiftTypes = new List<ShiftType>();
            using (var stream = excelStream.OpenReadStream())
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();

                // İlk satırı başlık satırı olarak kabul ediyoruz, bu yüzden atlıyoruz.
                foreach (var row in rows.Skip(1))
                {
                    var nameValue = row.Cell(1).GetValue<string>();
                    if (string.IsNullOrWhiteSpace(nameValue))
                        continue;
                    int startHour = row.Cell(2).GetValue<int>();
                    int startMinute = row.Cell(3).GetValue<int>();
                    int endHour = row.Cell(4).GetValue<int>();
                    int endMinute = row.Cell(5).GetValue<int>();
                    var color = row.Cell(6).GetValue<string>();
                    if (startHour > 23 || startMinute > 59 || endHour > 23 || endMinute > 59)
                    {
                        return Json(new { success = isSuccess, message = "Verileriniz bozuk lütfen excel dosyanızı kontrol edip tekrar deneyin!" });
                    }

                    var startTime = new TimeSpan(startHour, startMinute, 0);
                    var endTime = new TimeSpan(endHour, endMinute, 0);

                    var shiftType = new ShiftType
                    {
                        Name = row.Cell(1).GetValue<string>(),
                        StartTime = startTime,
                        EndTime = endTime,
                        Color = color,
                        ModifiedDate = DateTime.Now,
                        ModifiedByName = user.UserName
                    };

                    shiftTypes.Add(shiftType);
                }
            }
            var data = stm.BulkInsert(shiftTypes).Result;
            if(data.ResultStatus == ResultStatus.Success)
            {
                isSuccess = true;
            }
            return Json(new { success = isSuccess, message = data.Message });
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
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateShiftType(int? id)
        {
            var model = new ShiftTypeModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (id.HasValue)
            {
                var result = await stm.GetById(id.Value);
                var entity = result.Data;
                if (entity != null && result.ResultStatus == ResultStatus.Success)
                {
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.StartTime = entity.StartTime;
                    model.EndTime = entity.EndTime;
                    model.Color = entity.Color;
                    model.ModifiedByName = user != null ? user.Name : "";
                }
                TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
            }
            return PartialView(model);
        }

        public async Task<IActionResult> AddOrUpdateScheduleShift(string date, int? id)
        {
            var model = new ScheduleShiftModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.StartDate = DateTime.Parse(date);
            model.EndDate = model.StartDate;
            model.ShiftTypes = stm.GetAll().Result.Data;
            if (id.HasValue)
            {
                //güncelleme eklersem yapacağım ama şimdilik gereksiz geldi
                //var result = await stm.GetById(id.Value);
                //var entity = result.Data;
                //if (entity != null && result.ResultStatus == ResultStatus.Success)
                //{
                //    model.Id = entity.Id;
                //    model.ModifiedByName = user != null ? user.Name : "";
                //}
                //TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveShiftType(ShiftTypeModel model)
        {
            var message = "resultMessage";
            var id = model.Id;
            var newSt = new ShiftType();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            newSt.Name = model.Name;
            newSt.StartTime = model.StartTime;
            newSt.EndTime = model.EndTime;
            newSt.Color = model.Color;
            newSt.ModifiedByName = user != null ? user.Name : "";
            if (id != 0 && id != null)
            {
                newSt.Id = id;
                var result = stm.Update(newSt);
                if (!string.IsNullOrEmpty(result.Result.Message))
                {
                    message = result.Result.Message;
                }
            }
            if (id == 0)
            {
                newSt.CreatedByName = newSt.ModifiedByName;
                var result = stm.Add(newSt);
                if (!string.IsNullOrEmpty(result.Result.Message))
                {
                    message = result.Result.Message;
                }
            }
            if (id == null || model.Name == null)
            {
                message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
            }
            return Json(message);
        }

        public async Task<IActionResult> SaveScheduleShift(ScheduleShiftModel model)
        {
            var message = "resultMessage";
            var id = model.Id;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var newSt = new ShiftType();
            var newSh = new Shift();
            var newSs = new ScheduleShift();
            newSs.ModifiedByName = user != null ? user.Name : "";
            if (model.IsSpecial == true)
            {
                
                newSt.Name = model.SpecialShiftType;
                newSt.StartTime = model.SpecialStartTime;
                newSt.EndTime = model.SpecialEndTime;
                newSt.Color = model.SpecialColor;
                newSt.ModifiedByName = user != null ? user.Name : "";
                newSt.CreatedByName = newSt.ModifiedByName;
                model.ShiftTypeId = stm.Add(newSt).Result.Data.Id;
            }

            newSh.ShiftTypeId = model.ShiftTypeId;
            newSh.EmployeeId = model.EmployeeId;
            newSh.ModifiedByName = user != null ? user.Name : "";
            newSh.CreatedByName = newSh.ModifiedByName;
            var shift = await sm.Add(newSh);
            var shiftId = shift.Data.Id;

            if(shiftId != null && id == 0) 
            {
                newSs.CreatedByName = newSs.ModifiedByName;
                newSs.StartDate = model.StartDate;
                newSs.EndDate = model.EndDate;
                newSs.ShiftId = shiftId;
                var result = ssm.Add(newSs);
                if (!string.IsNullOrEmpty(result.Result.Message))
                {
                    message = result.Result.Message;
                }
            }

            if (id == null || shiftId == null)
            {
                message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
            }
            return Json(message);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShiftTypes(DepartmentModel model)
        {
            if (model.Id != null || model.Id != 0)
            {
                var shiftType = new ShiftType();
                shiftType.Id = model.Id;
                var result = await stm.Delete(shiftType);
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

        

        public IActionResult DeleteScheduleShift(ScheduleShiftModel ssModel)
        {
            var newSs = new ScheduleShift();

            newSs.Id = ssModel.Id;
            newSs.ModifiedByName = "mvcSend";


            ssm.Delete(newSs);

            return RedirectToAction("Index");
        }

        
        public IActionResult ShiftTypes()
        {
            return PartialView("ShiftTypes");
        }
    }
}
