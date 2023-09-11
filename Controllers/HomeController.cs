using EducationApp.Dtos;
using EducationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EducationApp.Controllers
{
    public class HomeController : Controller
    {


        //private readonly UserManager<Student> _studentManager;

        //private readonly UserManager<Instrocter> _instrocterManager;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _appDbContext;

        //UserManager<Student> studentManager, UserManager<Instrocter> instrocterManager
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _appDbContext = appDbContext;

            //_studentManager = studentManager;

            //_instrocterManager = instrocterManager;

            _logger = logger;

            _userManager = userManager;

            _roleManager = roleManager;

         

        
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            //var s=TempData["UserData"];

            try
            {

                string[] roleNames = { "Admin", "Student", "Instrocter" };

                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var admin = new IdentityUser
                {
                    UserName = "Admin",

                    EmailConfirmed= true,

                };

                var result = await _userManager.CreateAsync(admin, "Qwe123!");

				var res = await _userManager.AddToRoleAsync(admin, "Admin");

			}
            catch (Exception ex)
            {



            }
            //string userName = User.Identity.Name;

            //var user=await _userManager.FindByNameAsync(userName);

            //if (TempData["UserData"] == null)
            //    TempData["UserData"] = JsonConvert.SerializeObject(new UserViewData { Email = user.Email, Username = User.Identity.Name });



            //var res=_appDbContext.Enrollments.Where(x => x.Student == user).ToList();




            //var user = await _userManager.FindByNameAsync(User.Identity.Name); // Kullanıcının e-postasını buraya eklemelisiniz.

            //if (user != null)
            //{
            //    var res = await _userManager.AddToRoleAsync(user, "Instrocter"); // user.Id ve "Admin" rolü buraya uyarlanmalı.
            //}

            
            //}

            //var instrocter = new Instrocter
            //{
            //   UserName="TEST",



            //};



            //var result = await _instrocterManager.CreateAsync(instrocter, "!123123aA");

            ////var result2 = _appDbContext.Instrocters.Add(instrocter);

            //_appDbContext.SaveChanges();

            //if (result.Succeeded)
            //{
            //    // Kullanıcı başarıyla kaydedildi
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}