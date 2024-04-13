using Google.Authenticator;
using Zevopay.Contracts;

namespace Zevopay.Services
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        public string GenerateQrCode(string email, string secretKey)
        {
            var authenticatorUri = string.Format(
                "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&algorithm=SHA1&digits=6&period=30",
                Uri.EscapeDataString("Zevopay"),
                Uri.EscapeDataString(email),
                secretKey);

            return authenticatorUri;
        }

        public bool VerifyCode(string secretKey, string code)
        {
            var authenticator = new TwoFactorAuthenticator();
            return authenticator.ValidateTwoFactorPIN(secretKey, code);
        }
    }

}
