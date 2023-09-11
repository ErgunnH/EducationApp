using EducationApp.Dtos;
using EducationApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace EducationApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController


        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _userManager;




        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserLoginDto userLoginDto)
        {
              
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, userLoginDto.Remember, true);           

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginDto.Username);

                //TempData["UserData"] = JsonConvert.SerializeObject(new UserViewData
                //{
                //    Username = user.UserName,

                //    Email = user.Email
                //});

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        // GET: LoginController/Details/5
      
        [Authorize]  
        public async Task<ActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Login");
            // Kullanıcı oturum açık, işleme devam edebilirsiniz.


        }



    }     

    
}
