using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
       
        public UserManager<ApplicationUser> userManager { get; }

       public RoleManager<IdentityRole> roleManager { get; }
        private readonly SignInManager<ApplicationUser> signInManager;
       

        public RoleController(RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> AllRoles()
        {
            // Get all roles
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]

        public IActionResult NewRole()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> NewRole(RoleViewModel vm)
        {
            if(ModelState.IsValid==true)
            {
                IdentityRole roleModel= new IdentityRole();
                roleModel.Name=vm.RoleName;
                IdentityResult result=await roleManager.CreateAsync(roleModel);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description); 
                    }
                }
            }
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {//Mapping from vm to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = vm.Email;
                userModel.UserName = vm.UserName;
                userModel.Phone = vm.Phone;
                userModel.PasswordHash = vm.Password;

                IdentityResult result = await userManager.CreateAsync(userModel, vm.Password);
                if (result.Succeeded)
                {
                  await  userManager.AddToRoleAsync(userModel,"admin");
                 
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
                    await signInManager.SignInWithClaimsAsync(userModel, false, claims);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                }
            }
            return View(vm);
        }


    }   
}

