using Microsoft.AspNetCore.Identity;

namespace CinemaVR.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = "";
    }
}