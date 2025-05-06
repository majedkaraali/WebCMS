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
        public Patient Patient { get; set; } 

        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } 
        public string Status { get; set; }

 
        public string? ReasonForVisit { get; set; }
        public string? Symptoms { get; set; }
        public int? Duration { get; set; } 

        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now; 

        public DateTime? UpdatedDate { get; set; } 

        public bool? IsCancelled { get; set; } = false; 
    }

}
