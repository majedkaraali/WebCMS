using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models
{
    public class LabUser
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
       
        public ApplicationUser User { get; set; }

        public string? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }


    }
}

