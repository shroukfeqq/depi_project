using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.ViewModels;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
         this.userManager = userManager;
            this.signInManager = signInManager;
        }

     

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
    
        public async Task <IActionResult> Register( RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {//Mapping from vm to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = vm.Email;
                userModel.UserName= vm.UserName;  
                userModel.Phone=vm.Phone;
                userModel.PasswordHash = vm.Password;
             
                IdentityResult result = await userManager.CreateAsync(userModel,vm.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel,"user");
                   
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
                    await signInManager.SignInWithClaimsAsync(userModel,false,claims);
                    return RedirectToAction("Index","Home");
                    
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("Password",item.Description);
                    }
                }
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
              ApplicationUser userModel=await  userManager.FindByEmailAsync(vm.Email);
                if (userModel != null)
                {
                  bool found=  await userManager.CheckPasswordAsync(userModel,vm.Password);
                    if(found==true)
                    {
                        //cookiee
                      //  signInManager.SignInAsync(userModel, false);//id user role
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, userModel.Email));// 4-email
                        await signInManager.SignInWithClaimsAsync(userModel, vm.RememberMe, claims);
                        if (User.IsInRole("admin"))
                        {
                            return RedirectToAction("AdminPanel", "AdminPanel");
                        }
                        else //if (User.IsInRole("user"))
                        {
                            return RedirectToAction("MatchList", "Content");
                        }
                    }
                   else
                        ModelState.AddModelError("", "Password is wrong ");
                }
                ModelState.AddModelError("", "Email is not found");
            }
            return View(vm);
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return View("Login");
        }
     
    }
}
