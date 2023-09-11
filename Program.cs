using EducationApp.Dtos;
using EducationApp.Models;
using EducationApp.Services;
using EducationApp.ValidationRules;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddMvc();


    
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;   

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();




builder.Services.ConfigureApplicationCookie(options =>
{
    

    // Yetkilendirme ba�ar�s�z oldu�unda y�nlendirilecek sayfa
    options.LoginPath = "/Login";

    options.LogoutPath = "/Login";

    options.AccessDeniedPath = "/Login";

    options.ReturnUrlParameter = "";

    //oturum s�resi 30dk
    options.ExpireTimeSpan= TimeSpan.FromMinutes(30);
    //her istekde oturum s�resi yenilensinmi
    options.SlidingExpiration = true;

  
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Kilitlenme ayarlar�
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;


    // Kullan�c� ad� ve e-posta do�rulama ayarlar�
    options.User.RequireUniqueEmail = false;


    // �ki fakt�rl� kimlik do�rulama ayarlar�
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;


});


builder.Services.AddScoped<IValidator<UserRegisterDto>, UserValidationRules>();

builder.Services.AddScoped<IValidator<InstrocterFileUploadDto>, InstrocterFileUploadValidationRules>();

builder.Services.AddScoped<IFileValidationServices, FileValidation>();

builder.Services.AddScoped<IFileUploadServices, FileUpload>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
