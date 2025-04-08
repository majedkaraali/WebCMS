using System;
using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } // Navigation property  

        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } // Navigation property  

        public string Status { get; set; }

        // New properties  
        public string? ReasonForVisit { get; set; }
        public string? Symptoms { get; set; }
        public int? Duration { get; set; } // duration in minutes  

        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Automatically set to now  

        public DateTime? UpdatedDate { get; set; } // Nullable for optional updates  

        public bool? IsCancelled { get; set; } = false; // Default to not cancelled  
    }

}
