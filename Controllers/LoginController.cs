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


            //Role göre Controllerlara yönlendirme yapıyor
              
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, userLoginDto.Remember, true);           

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginDto.Username);     
                
                var role=await _userManager.GetRolesAsync(user);

                return RedirectToAction("Index", role[0]);
            }


            return View();
        }

        
      
        [Authorize]  
        public async Task<ActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Login");
            // Kullanıcı oturum açık, işleme devam edebilirsiniz.


        }



    }     

    
}
