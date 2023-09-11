using EducationApp.Dtos;
using EducationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace EducationApp.Controllers
{

    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _appDbContext;

        public StudentController(UserManager<IdentityUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {

            //Kayıt Yaptırmadıgı eğitimler listeleniyor

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var student = _appDbContext.Students.Include(studnet => studnet.Enrollments).FirstOrDefault(x => x.Id == user.Id);
            
            var trainings = _appDbContext.Trainings.Where(y => !y.Enrollments.Any(z => student.Enrollments.Contains(z)) && y.StartDate > DateTime.Now).Select(x => new StudentTrainingDto
            {
                TrainingId = x.TrainingId,

                Title = x.Title,

                FilePath = x.FilePath,


            }).ToList();

            return View(trainings);
        }



        public async Task<IActionResult> EducationPurchase(int id)
        {

            //Eğitim Detayları Satın Alma Sayfası

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var student = _appDbContext.Students.Include(studnet => studnet.Enrollments).FirstOrDefault(x => x.Id == user.Id);
            
            var training = _appDbContext.Trainings.Select(x => new StudentTrainingPurchaseDetail
            {

                TrainingId = x.TrainingId,

                Title = x.Title,

                FilePath = x.FilePath,

                StartDate = x.StartDate,

                EndDate = x.EndDate,

                Cost = x.Cost,

                SumCost = x.Cost,

                Quota = x.Quota,

                Enrollments = x.Enrollments,

                Contents = x.Contents,

            }).FirstOrDefault(y => (y.TrainingId == id) && !y.Enrollments.Any(z => student.Enrollments.Contains(z)));


            return View(training);
        }

        public async Task<IActionResult> Buy(int id)
        {

            //Satın Alma satın alma başarılı ise Enrollments tablosuna eklme yapıyor

            var IsPaymentOk = true;

            if (IsPaymentOk)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var student = _appDbContext.Students.Include(st => st.Enrollments.Where(e=>e.IsCancelled==false)).FirstOrDefault(x => x.Id == user.Id);               

                var training = _appDbContext.Trainings.Include(st => st.Enrollments.Where(e => e.IsCancelled == false)).FirstOrDefault(x => x.TrainingId == id);

                if (training.Enrollments.Count  < training.Quota)
                {
                    await _appDbContext.Enrollments.AddAsync(new Enrollment
                    {
                        StudentId = student.Id,

                        TrainingId = id,

                        StartDate = DateTime.Now,

                        IsCompleted = false,

                        IsCancelled= false,

                    });

                    var res = _appDbContext.SaveChanges();

                    if (res > 0)
                        return RedirectToAction("Index", "Student");

                    else
                        ModelState.AddModelError("", "Kayıdınız eklenirken beklenmeyen bir hata oluştu Lütfen Tekrar Deneyin");
                }
                else
                {
                    ModelState.AddModelError("", "Eğitimin Kontenjan sayısı dolmuştur.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Ödeme İşleminde beklenmeyen bir hata oluştu Lütfen Tekrar Deneyin");
            }


            return RedirectToAction("Index", "Student");

        }



        public async Task<IActionResult> EducationProfile()
        {

            //Profil sayfası bitirilen ve devam eden eğitimler burada

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var student = _appDbContext.Students.Include(st => st.Enrollments.Where(e=>e.IsCancelled==false)).FirstOrDefault(x => x.Id == user.Id);


            var continueTrainings = _appDbContext.Trainings.Where(y => (y.EndDate >= DateTime.Now) && y.Enrollments.Any(z => student.Enrollments.Contains(z))).Select(x => new StudentTrainingDto
            {
                TrainingId = x.TrainingId,

                Title = x.Title,

                FilePath = x.FilePath,



            }).ToList();

            var completedTrainings = _appDbContext.Trainings.Where(y => (y.EndDate < DateTime.Now) && y.Enrollments.Any(z => student.Enrollments.Contains(z))).Select(x => new StudentTrainingDto
            {
                TrainingId = x.TrainingId,

                Title = x.Title,

                FilePath = x.FilePath,

            }).ToList();


            return View(new StudentTrainingProfilDto
            {
                CompletedTrainings = completedTrainings,

                ContinueTrainings = continueTrainings

            });
        }


        public async Task<IActionResult> EducationProfileDetail(int id)
        {
            
            //Profilindeki eğitimin içeriklerini listeler (pdf-mp4-jpg ...)

            var contents = _appDbContext.Contents.Where(x => x.TrainingId == id).Select(y => new InstrocterContentDto
            {
                ContentId = y.ContentId,

                ContentName = y.ContentName,

                ContentPath = y.ContentPath,

                ContentType = y.ContentType,

            }).ToList();

            return View(contents);
        }

        public async Task<IActionResult> CancelledEnrolmentRequest(int id)
        {

            //Eğitime katılım taleplerinin iptal edilmesini kontrol eder

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var student = _appDbContext.Students.Include(st => st.Enrollments).FirstOrDefault(x => x.Id == user.Id);

            var training = _appDbContext.Trainings.FirstOrDefault(x => x.TrainingId == id);           


            if (training.StartDate > DateTime.Now)
            {
                student.Enrollments.FirstOrDefault(x => x.TrainingId == id).IsCancelled = true;

                var res=_appDbContext.SaveChanges();

                if (res > 0)
                    return RedirectToAction("EducationProfile", "Student");

                else
                    TempData["error"]="Kayıdınız silinirken beklenmeyen bir hata oluştu Lütfen Tekrar Deneyin";
            }

            else
            {
                TempData["error"] = "Başlamış bir eğitimi iptal edemessin";
            }

            return RedirectToAction("EducationProfile", "Student");
        }
    }
}
