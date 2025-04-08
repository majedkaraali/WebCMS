namespace WebCMS.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public ApplicationUser User { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? MedicalHistory { get; set; }
        public List<Appointment> Appointments { get; set; }

        // New properties  
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? PolicyNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Allergies { get; set; }
        public string? SocialHistory { get; set; }
    }

}
