using Zevopay.Models;

namespace Zevopay.Contracts
{
    public interface IAccountService
    {
        Task<ResponseModel> Login(LoginModel model);
        void Logout();

        //Task<ResponseModel> ResetPassword(ResetPasswordModel model);

    }
}
