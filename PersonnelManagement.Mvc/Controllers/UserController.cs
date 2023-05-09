using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Entities.DTOs;
using PersonnelManagement.Mvc.Models;

namespace PersonnelManagement.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IImageHelper _imageHelper;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager
            /*ILogLoginService logLoginService,*/ )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_logLoginService = logLoginService;
        }
        /// <summary>
        /// Authorize ekleyeceğiz.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var model = new UserViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Name = user.Name;
            model.Surname = user.Surname;
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, dto.Password,
                        dto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        
                        ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlıştır.");
                        return View("UserLogin");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlıştır.");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
        }
    }
}
