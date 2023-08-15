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
        private readonly RoleManager<Role> _roleManager;
        public ProfileController(UserManager<User> userManager, IScheduleShiftService _ssm, IShiftService _sm, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            ssm = _ssm;
            sm = _sm;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new UserModel();
            model.UserName = user.UserName;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.PictureUrl = "~/img/pp/" + user.Picture;
            model.EmployeeId = user.EmployeeId;
            return View(model);
        }

        public JsonResult GetCalendarDatas()
        {
            List<CalendarDto>? data;
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.EmployeeId != null)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel model)
        {
            bool isSuccess = false;
            string rMessage = rMessage = "Şifreleriniz uyuşmuyor lütfen kontrol edin!";
            if (model.Password == model.ConfirmPassword && model.CurrentPassword != null)
            {
                var newUser = new User();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                string newImageName;
                if (model.PictureName != null)
                {
                    newImageName = model.PictureName;
                }
                else
                {
                    newImageName = "default.jpg";
                }

                var id = user.Id;
                if (model.Name != null && model.Name != "")
                {
                    if (id != 0 && id != null)
                    {
                        newUser = await _userManager.FindByIdAsync(id.ToString());
                    }
                    var passwordCheck = CreatePasswordHash(newUser, model.CurrentPassword);
                    if (newUser.PasswordHash == passwordCheck)
                    {
                        //--Picture--
                        if (model.Picture != null)
                        {
                            var extension = Path.GetExtension(model.Picture.FileName);
                            newImageName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_" + user + extension;
                            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pp/", newImageName);

                            var stream = new FileStream(location, FileMode.Create);

                            model.Picture.CopyTo(stream);

                            newUser.Picture = newImageName;

                        }
                        //---Password--
                        if (model.Password != null)
                        {
                            newUser.PasswordHash = CreatePasswordHash(newUser, model.Password);
                        }
                        if (id != 0 && id != null)
                        {
                            var data = await _userManager.UpdateAsync(newUser);
                            if (data.Succeeded)
                            {
                                isSuccess = true;
                                rMessage = "Hesap bilgileriniz başarıyla güncellendi";
                            }
                        }
                        if (id == 0)
                        {
                            //await _userManager.CreateAsync(newUser);
                            //var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                            //await _userManager.AddToRoleAsync(newUser, role.Name);
                            rMessage = "Giriş yapan kullanıcı doğrulanamadı!";
                        }
                        if (id == null || model.Name == null)
                        {
                            rMessage = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
                        }
                    }
                    else
                    {
                        rMessage = "Mevcut şifreniz yanlış! Lütfen kontrol edip tekrar deneyiniz!";
                    }
                }
            }
            return Json(new { success = isSuccess, message = rMessage });
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }

        public async Task<IActionResult> UpdateWorkStatus(int shiftId, bool isStart)
        {
            var shift = await sm.GetById(shiftId);
            if (shift != null)
            {
                if (isStart)
                {
                    //saati geldi mi kontrol değilse json gritter
                    shift.Data.Enter = DateTime.Now;
                }
                else
                {
                    //saati geldi mi kontrol değilse json gritter
                    shift.Data.Exit = DateTime.Now;
                }
                var result = await sm.Update(shift.Data);
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return Json(null, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
