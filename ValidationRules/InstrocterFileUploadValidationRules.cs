using EducationApp.Dtos;
using EducationApp.Enums;
using FluentValidation;

namespace EducationApp.ValidationRules
{
    public class InstrocterFileUploadValidationRules: AbstractValidator<InstrocterFileUploadDto>
    {
        public InstrocterFileUploadValidationRules()
        {


            RuleFor(x => (int)x.Category).NotNull().WithMessage("Category Alanını Boş Geçmeyiniz");

            RuleFor(x => (int)x.Category).LessThan(Enum.GetNames(typeof(CategoryEnum.Category)).Length).WithMessage("Geçerli bir Category Alanını Seçini Lütfen");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanını Boş Geçmeyiniz");

            RuleFor(x => x.Cost).NotEmpty().WithMessage("Miktar Alanını Boş Geçmeyiniz");

            RuleFor(x => x.Cost).GreaterThanOrEqualTo(0).WithMessage("Fiyat alanı 0'dan küçük olamaz");

            RuleFor(x => x.StartDate).GreaterThan(DateTime.Now).WithMessage("Başlangıç Tarihi Geçmiş bir Tarihinden Olamaz");

            RuleFor(x => x.StartDate).LessThan(y=>y.EndDate).WithMessage("Başlık Tarihi Bitiş Tarihinden Önce olmalı");            

            RuleFor(x => x.Quota).NotEmpty().WithMessage("Kontenjan Alanını Boş Geçmeyiniz");

            RuleFor(x => x.Quota).GreaterThan(0).WithMessage("Lütfen Kontenjan değeri 0'dan büyük bir değer giriniz");   

        }










    }
}
