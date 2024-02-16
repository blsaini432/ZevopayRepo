using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;


namespace Zevopay.Controllers.MVC
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, IAccountService accountService, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _accountService = accountService;
            this.roleManager = roleManager;
        }

        #region Login/Logout Action 
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _accountService.Login(model));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            _accountService.Logout();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return View(model);
          //  return PartialView("_AddUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            
                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }
            
            return View(model);
        }

        #region Forgot/Reset Password
        /* [AllowAnonymous]
         public IActionResult ForgotPassword()
         {
             return View();
         }*/

        /*[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                    if (user == null) return Json(new ResponseModel { ResultFlag = 0, Message = "User Not Found" });
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account",
                        new { email = user.Email, token = token }, protocol: HttpContext.Request.Scheme);

                    // Prepare the email content here, including the button with the callbackUrl
                    var emailContent = $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.";

                    await _emailService.SendEmailAsync(user.Email, "Reset Password", emailContent);

                    return Json(new ResponseModel { ResultFlag = 1, Message = "Please check your email to reset your password." });
                }
                else
                {
                    return Json(new ResponseModel { ResultFlag = 0, Message = "Invalid Email ID" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error! while excuting action {nameof(ForgotPassword)} errormessage '{ex.Message}'");
            }
            return Json(new ResponseModel { ResultFlag = 0, Message = "User Not Found" });
        }*/

        /*[HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }*/

        /*[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _accountService.ResetPassword(resetPasswordModel));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error! while excuting action {nameof(ResetPassword)} errormessage '{ex.Message}'");
            }
            return Json(new ResponseModel { ResultFlag = 0, Message = "Failed" });
        }*/
        #endregion
    }


}
