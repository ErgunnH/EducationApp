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

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _appDbContext = appDbContext;

            _logger = logger;

            _userManager = userManager;

            _roleManager = roleManager;




        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

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


                string pass = "Qwe123!";


                var admin = new IdentityUser
                {
                    UserName = "Admin",

                    EmailConfirmed = true,

                };

                var student = new Student
                {
                    UserName = "Student",

                    EmailConfirmed = true,

                };

                var instrocter = new Instrocter
                {
                    UserName = "Instrocter",

                    EmailConfirmed = true,

                };

                //var user = await _userManager.FindByNameAsync(admin.UserName);

                var res1=await _userManager.CreateAsync(admin, pass);

                var res2 = await _userManager.CreateAsync(student, pass);

                var res3 = await _userManager.CreateAsync(instrocter, pass);

                var res4 = await _userManager.AddToRoleAsync(admin, "Admin");

                var res5 = await _userManager.AddToRoleAsync(student, "Student");

                var res6 = await _userManager.AddToRoleAsync(instrocter, "Instrocter");

                return View(model:@"{""username""=""Admin"",""Password""=" + pass + @",""UserRole""=""Admin"",
                                    ""username"" = ""Student"", ""Password"" = " + pass + @",""UserRole"" = ""Student"",
                                    ""username"" = ""Instrocter"", ""Password"" = " + pass + @", ""UserRole"" = ""Instrocter""}");
            }
            catch (Exception ex)
            {

                return View(model: ex.Message);

            }

             
            
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