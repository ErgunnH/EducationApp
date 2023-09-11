using AutoMapper;
using EducationApp.Dtos;
using EducationApp.Enums;
using EducationApp.Models;
using EducationApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static EducationApp.Enums.CategoryEnum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducationApp.Controllers
{

    [Authorize(Roles = "Instrocter")]
    public class InstrocterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _appDbContext;

        private readonly IValidator<InstrocterFileUploadDto> _validator;

        private readonly IFileValidationServices _fileValidator;

        private readonly IFileUploadServices _fileUpload;



        public InstrocterController(UserManager<IdentityUser> userManager, AppDbContext appDbContext, IValidator<InstrocterFileUploadDto> validator,
            IFileValidationServices fileValidationServices, IFileUploadServices fileUploadServices)
        {
            _userManager = userManager;

            _appDbContext = appDbContext;

            _validator = validator;

            _fileValidator = fileValidationServices;

            _fileUpload = fileUploadServices;

        }


        public async Task<IActionResult> Index()
        {


            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<InstrocterTrainingDto> trainings = new();

            if (user != null)
            {
                trainings = await _appDbContext.Trainings.Where(x => x.InstrocterId == user.Id).Select(x => new InstrocterTrainingDto
                {
                    TrainingId = x.TrainingId,
                    Title = x.Title,
                    FilePath = x.FilePath,
                    Cost = x.Cost
                }).ToListAsync();

            }


            return View(trainings);
        }

      
        public IActionResult AddEducation()
        {





            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddEducation(InstrocterFileUploadDto fileData, IFormFile file)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var validationResult = await _validator.ValidateAsync(fileData);

                var fileValidationResult = _fileValidator.ValidateResult(file, user.Id, fileData.Title, "jpeg", "jpg", "png");

                if (validationResult.IsValid && fileValidationResult.IsValid)
                {

                    string result = _fileUpload.Save(file, user.Id, fileData.Title);

                    _appDbContext.Trainings.Add(new Training
                    {
                        InstrocterId = user.Id,

                        Title = fileData.Title,

                        Category = fileData.Category,

                        Quota = fileData.Quota,

                        Cost = fileData.Cost,

                        StartDate = fileData.StartDate,

                        EndDate = fileData.EndDate,

                        FilePath = result

                    });

                    var dbResult = _appDbContext.SaveChanges();

                    if (dbResult > 0)
                        return RedirectToAction("Index", "Instrocter");

                    else
                        ModelState.AddModelError("", "Kayıdınız eklenirken beklenmeyen bir hata oluştu Lütfen Tekrar dDeneyin");
                }

                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);

                    }

                    foreach (var error in fileValidationResult.Errors)
                    {
                        ModelState.AddModelError("", error);

                    }


                }

            }

            return View();
        }



        public async Task<IActionResult> UpdateEducation(int id)
        {

            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var contets = await _appDbContext.Contents.Where(x => x.TrainingId == id).Select(x => new InstrocterContentDto
            {
                ContentId = x.ContentId,
                TrainingId = x.TrainingId,
                ContentName = x.ContentName,
                ContentPath = x.ContentPath,
                ContentType= x.ContentType,

            }).ToListAsync();



            

            return View(contets);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEducation(IFormFile file,string contentName,int id)
        {

            var training = await _appDbContext.Trainings.Select(x=> new InstrocterTrainingDto
            {
                TrainingId = x.TrainingId,
                Title = x.Title,
                FilePath = x.FilePath,
                Cost = x.Cost
                

            }).FirstOrDefaultAsync(y=>y.TrainingId==id);


            var fileValidationResult = _fileValidator.ValidateResult(file, training.FilePath.Split("/")[0], training.Title, contentName, "mp4", "jpeg","jpg","png","pdf");

            if (fileValidationResult.IsValid)
            {
                string result = _fileUpload.Save(file, training.FilePath.Split("/")[0], training.Title, contentName);

                _appDbContext.Contents.Add(new Content
                {
                    TrainingId = training.TrainingId,

                    ContentName = contentName,

                    ContentType = file.ContentType,

                    ContentPath = result,                   


                });

                var dbResult = _appDbContext.SaveChanges();

                if (dbResult > 0)
                    return RedirectToAction("UpdateEducation", "Instrocter");

                else
                    ModelState.AddModelError("", "Kayıdınız eklenirken beklenmeyen bir hata oluştu Lütfen Tekrar dDeneyin");

            }

            else
            {
                foreach (var error in fileValidationResult.Errors)
                {
                    ModelState.AddModelError("", error);

                }
            }

            return View();
        }
    }
}
