using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Areas.Admin.Models;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Services.Concrete;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace PersonnelManagement.Mvc.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IScheduleShiftService ssm;
        private readonly IShiftService sm;
        public ProfileController(UserManager<User> userManager, IScheduleShiftService _ssm, IShiftService _sm)
        {
            _userManager = userManager;
            ssm = _ssm;
            sm = _sm;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new UserModel();
            model.UserName = user.UserName;
            model.Name = user.Name;
            model.Surname= user.Surname;
            model.Email = user.Email;
            model.PhoneNumber= user.PhoneNumber;
            model.PictureUrl = "~/img/pp/" + user.Picture;
            model.EmployeeId = user.EmployeeId;
            return View(model);
        }

        public JsonResult GetCalendarDatas()
        {
            List<CalendarDto>? data;
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if(user.EmployeeId != null)
            {
                data = ssm.GetAllById(user.EmployeeId.Value).Result.Data;
                return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                return Json(null, new Newtonsoft.Json.JsonSerializerSettings());
            }
            
        }

        public JsonResult GetNextShift()
        {
            NextShiftDetailsDto? data;
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.EmployeeId != null)
            {
                data = ssm.GetNext(user.EmployeeId.Value).Result.Data;
                return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                return Json(null, new Newtonsoft.Json.JsonSerializerSettings());
            }

        }

        public async Task<IActionResult> UpdateWorkStatus(int shiftId, bool isStart)
        {
            var shift = await sm.GetById(shiftId);
            if (shift != null)
            {
                if(isStart)
                {
                    shift.Data.Enter = DateTime.Now;
                }
                else
                {
                    shift.Data.Exit = DateTime.Now;
                }
                var result = await sm.Update(shift.Data);
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return Json(null, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
