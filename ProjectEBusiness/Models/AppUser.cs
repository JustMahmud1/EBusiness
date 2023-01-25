using Microsoft.AspNetCore.Identity;

namespace ProjectEBusiness.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
