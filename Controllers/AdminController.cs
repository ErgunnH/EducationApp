using EducationApp.Dtos;
using EducationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EducationApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;    


        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
           
        }
        public IActionResult Index()
        {

            var users = _userManager.Users.ToList();

            var AdminUserList = users.Select(u => new AdminUserListDto
            {

                Username = u.UserName,

                Email = u.Email,

                Role = String.Join("-", _userManager.GetRolesAsync(u).Result.ToList()),

            }).ToList();

            return View(AdminUserList);
        }

        public async Task<IActionResult> RoleDisapprove(string id)
        {

            var user = await _userManager.FindByNameAsync(id);

            var userRoles =await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            return RedirectToAction("Index", "Admin");
        }
        public IActionResult RoleApprove()
        {

            var users = _userManager.Users.ToList();

            string role = "";

            var AdminUserList = users.Select(u =>
            {
                role = String.Join("-", _userManager.GetRolesAsync(u).Result.ToList());

                if (role == string.Empty)
                {
                    return new AdminUserListDto
                    {

                        Username = u.UserName,

                        Email = u.Email,

                        Role = role,
                    };
                }
                else return null;
            }
            ).ToList();

            while (AdminUserList.Remove(null)) ;

            return View(AdminUserList);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
           var user=await _userManager.FindByNameAsync(id);
            
            await  _userManager.DeleteAsync(user);

            return RedirectToAction("RoleApprove","Admin");
        }
    }
}
