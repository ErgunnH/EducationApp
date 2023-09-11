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



                var admin = new IdentityUser
                {
                    UserName = "Admin",

                    EmailConfirmed= true,

                };

				var user=await _userManager.FindByNameAsync(admin.UserName);

                if (user == null)
                {
					await _userManager.CreateAsync(admin, "Qwe123!");

					await _userManager.AddToRoleAsync(admin, "Admin");
				}

			



			}
            catch (Exception ex)
            {



            }


			return View(model: @"{""username""=""Admin"",""Password""=""Qwe123!"",""UserRole""=""Admin"",""Roles"":[""Admin"", ""Student"", ""Instrocter""]}");
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