using Microsoft.AspNetCore.Identity;
namespace WebCMS.Models

{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // Patient, Doctor, Admin
    }
}
