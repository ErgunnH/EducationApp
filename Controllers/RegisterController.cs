using EducationApp.Dtos;
using EducationApp.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: RegisterController

        private readonly UserManager<IdentityUser> _userManager;

		

		private readonly IValidator<UserRegisterDto> _validator;
        public RegisterController(UserManager<IdentityUser> userManager, IValidator<UserRegisterDto> validator, )
        {
            
            _userManager = userManager;

            _validator = validator;       

        }


        [HttpGet]
        public ActionResult Index()
        {   

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserRegisterDto userRegisterDto)
        {

            var validationResult = await _validator.ValidateAsync(userRegisterDto);

            if (validationResult.IsValid)
            {
              
                IdentityResult result= new IdentityResult();

				IdentityResult resRole = new IdentityResult();

				if (userRegisterDto.Role== "Instrocter")
                {
					Instrocter instrocter = new Instrocter
					{
						UserName = userRegisterDto.Username,
                        Email = userRegisterDto.Email,
                        EmailConfirmed = true
                    };


					result = await _userManager.CreateAsync(instrocter, userRegisterDto.Password);

					var res = await _userManager.AddToRoleAsync(instrocter, "Instrocter");


				}
                else if(userRegisterDto.Role == "Student")
                {
					Student student = new Student
					{
						UserName = userRegisterDto.Username,
						Email = userRegisterDto.Email,
						EmailConfirmed = true
					};

					result = await _userManager.CreateAsync(student, userRegisterDto.Password);

					resRole = await _userManager.AddToRoleAsync(student, "Student");
				}

             
				               

                if (result.Succeeded )
                {					

					return RedirectToAction("Index", "Login");

                }

                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

					foreach (var error in resRole.Errors)
					{
						ModelState.AddModelError("", error.Description);

					}
				}
            }

            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);

                }

            }




            return View();
        }





        // GET: RegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
