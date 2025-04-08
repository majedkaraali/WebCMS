namespace WebCMS.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Specialization { get; set; }
        public string? Availability { get; set; }
        public List<Appointment> Appointments { get; set; }
    }

}
