namespace Zevopay.Contracts
{
    public interface ITwoFactorAuthService
    {
        string GenerateQrCode(string userName, string secretKey);
        bool VerifyCode(string secretKey, string code);
    }
}
