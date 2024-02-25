using Microsoft.AspNetCore.Identity;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ResponseModel> Login(LoginModel model)
        {
            ResponseModel response = new ResponseModel();
            var user = await _userManager.FindByEmailAsync(model.Email.Trim());
            if (user == null)
            {
                return response = new ResponseModel { Data = "", ResultFlag = 0, Message = "User not found!" };
            }
            else if (await _userManager.CheckPasswordAsync(user, model.Password.Trim()) == false)
            {
                return response = new ResponseModel { ResultFlag = 0, Message = "Invalid credentials!" };
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password.Trim(), model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                response = new ResponseModel { ResultFlag = 1, Message = "success!" };
            }
            else if (result.IsLockedOut)
            {
                response = new ResponseModel { ResultFlag = 0, Message = "Your account is not active!" };
            }
            else
            {
                response = new ResponseModel { ResultFlag = 0, Message = "Invalid login attempt!" };
            }
            return response;
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

        /*public async Task<ResponseModel> ResetPassword(ResetPasswordModel model)
        {
            ResponseModel response = new ResponseModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                response = new ResponseModel { ResultFlag = 0, Message = "Invalid Eamil!" };
            }
            else
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (!resetPassResult.Succeeded)
                {
                    var errorMessages = new List<string>();
                    foreach (var error in resetPassResult.Errors)
                    {
                        errorMessages.Add(error.Description);
                    }
                    response = new ResponseModel
                    {
                        ResultFlag = 0, // Assuming 0 indicates failure
                        Message = string.Join(", ", errorMessages)
                    };
                }
                else
                {
                    response = new ResponseModel { ResultFlag = 1, Message = "Password is updated successfully " };
                }
            }
            return response;
        }*/

    }






}
