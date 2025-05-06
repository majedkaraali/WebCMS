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

        public bool updated { get; set; } = false;
  
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? PolicyNumber { get; set; }

        public string? SocialSecurityNumber { get; set; }
        public string? CompanyName { get; set; }

        public int? UserHeight { get; set; } // in cm
        public int? Weight { get; set; } // in kg
        public string? BloodType { get; set; }



        public List<int>? SelectedAllergyIds { get; set; } = new List<int>();


        //Social History
        public string? Smoking { get; set; }
        public string? Alcohol { get; set; }
        public string? SporExercise { get; set; }
        public string? Occupation { get; set; }


    }

}
