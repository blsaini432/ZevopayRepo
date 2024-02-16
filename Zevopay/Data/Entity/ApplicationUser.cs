using Microsoft.AspNetCore.Identity;

namespace Zevopay.Data.Entity
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
