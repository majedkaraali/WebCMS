using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models;
public class Prescription
{
    [Key]
    public int Id { get; set; }

    public int DoctorId { get; set; } 
    public int PatientId { get; set; } 

    [Required]
    public int AppointmentId { get; set; }  
    public Appointment Appointment { get; set; }  


    public string MedicationName { get; set; }

    public string Dosage { get; set; } 

    public string Frequency { get; set; } // E.g., "Twice a day"  

    public string Duration { get; set; } // E.g., "7 days"  

    public DateTime CreatedDate { get; set; } = DateTime.Now; // Automatically set to now  

    public string Notes { get; set; } // Any additional information about the prescription  
}  