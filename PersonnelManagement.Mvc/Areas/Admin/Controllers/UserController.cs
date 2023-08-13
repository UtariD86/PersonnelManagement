using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.Dtos;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Controllers;
using System.Text.Json.Serialization;
using System.Text.Json;
using zurafworks.Shared.Results.ComplexTypes;
using PersonnelManagement.Mvc.Models;
using PersonnelManagement.Mvc.Areas.Admin.Models;
using PersonnelManagement.Mvc.Helpers.Abstract;

namespace PersonnelManagement.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPhoneCodesHelper _phoneCodesHelper;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IPhoneCodesHelper phoneCodesHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _phoneCodesHelper = phoneCodesHelper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password,
                        userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.Where(u => u.IsDeleted == false).ToListAsync();
            return Json(users);
        }
        
        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public async Task<JsonResult> GetPhoneCodes()
        //{
        //    var phoneCodes = 
        //    return Json(phoneCodes);
        //}

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateUser(int? id)
        {
            
            var model = new UserModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _roleManager.Roles.ToListAsync();
            var phoneCodesFilePath = @"wwwroot\js\datas\phoneCodes.json";
            var picturePath = "~/img/pp/";
            var countryCodes = await _phoneCodesHelper.GetPhoneCodesFromFile(phoneCodesFilePath);
            model.CountryCodes = countryCodes;
            model.Roles = roles;
            if (id.HasValue)
            {
                var selectedUser = await _userManager.FindByIdAsync(id.Value.ToString());
                var userRole = _userManager.GetRolesAsync(selectedUser).Result.FirstOrDefault();
                if (selectedUser != null)
                {
                    var phoneNumber = selectedUser.PhoneNumber;
                    model.Id = selectedUser.Id;
                    model.UserName = selectedUser.UserName;
                    model.Name = selectedUser.Name;
                    model.Surname = selectedUser.Surname;
                    model.Email = selectedUser.Email;
                    model.PhoneNumber = selectedUser.PhoneNumber;
                    model.Dial_Code = phoneNumber.Length > 10 ? phoneNumber.Substring(0, phoneNumber.Length - 10) : phoneNumber;
                    model.PhoneNumber = phoneNumber.Length > 10 ? phoneNumber.Substring(phoneNumber.Length - 10) : phoneNumber;
                    model.PictureUrl = picturePath + selectedUser.Picture;
                    model.PictureName = selectedUser.Picture;
                    model.EmployeeId = selectedUser.EmployeeId;
                    int roleId;
                    for(int i=0; i<roles.Count ; i++)
                    {
                        if (roles[i].Name == userRole) 
                        {
                            roleId = roles[i].Id;
                            model.RoleId = roleId;
                        }
                    }
                }
                TempData["OperationType"] = " Guncelleme ";
            }
            else
            {
                TempData["OperationType"] = " Veritabanına Kaydetme ";
                model.Dial_Code = "+90";
                model.PictureUrl = picturePath+"default.jpg";
            }
            return PartialView(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel model)
        
        {
            var message = "resultMessage";
            string newImageName;
            if (model.PictureName != null)
            {
                newImageName = model.PictureName;
            }
            else
            {
                newImageName = "default.jpg";
            }

            var id = model.Id;
            var newUser = new User();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (model.Name != null && model.Name != "")
            {
                if (id != 0 && id != null)
                {
                    newUser = await _userManager.FindByIdAsync(id.ToString());
                }
                newUser.UserName = model.UserName;
                newUser.NormalizedUserName = _userManager.NormalizeName(model.UserName);
                newUser.Name = model.Name;
                newUser.Surname = model.Surname;
                newUser.Email = model.Email;
                newUser.NormalizedEmail = _userManager.NormalizeEmail(model.Email);
                newUser.PhoneNumber = model.Dial_Code + model.PhoneNumber;
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
                //----Employee--
                if(model.EmployeeId != null)
                {
                    newUser.EmployeeId = model.EmployeeId;
                }
                if (id != 0 && id != null)
                {
                    await _userManager.UpdateAsync(newUser);
                }
                if (id == 0)
                {
                    await _userManager.CreateAsync(newUser);
                    var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                    await _userManager.AddToRoleAsync(newUser, role.Name);
                }
                if (id == null || model.Name == null)
                {
                    message = "Kaydetmeye çalıştığınız veriler kayıp ya da bulunamıyor.";
                }
            }
            return Json(message);
        }



        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUsers(UserModel model)
        {
            if (model.Id != null || model.Id != 0)
            {
                var user = _userManager.FindByIdAsync(model.Id.ToString()).Result;
                if(user != null) 
                {
                    user.IsDeleted = true;
                    await _userManager.UpdateAsync(user);
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    TempData["PopupMessage"] = result.Message;
                    //}
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
