using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models;
public class Prescription
{
    [Key]
    public int Id { get; set; }

    public int DoctorId { get; set; } 
    public int PatientId { get; set; } 

    public Doctor doctor { get; set; }
    public Patient patient { get; set; }


    [Required]
    public int AppointmentId { get; set; }  
    public Appointment Appointment { get; set; }  

    public string Status { get; set; } // Active, Discontinued, etc.
    public string MedicationName { get; set; }

    public string Quantity { get; set; } // E.g., "30 tablets" , "1 bottle"

    public string Dosage { get; set; } // E.g., "500 mg" , "1 tablet"

    public string Frequency { get; set; } // E.g., "Twice a day"  

    public string Duration { get; set; } // E.g., "7 days"  

    public string? ForDiseases { get; set; } // E.g., "Hypertension" , "Diabetes"

    public DateTime CreatedDate { get; set; } = DateTime.Now; // Automatically set to now  

    public string Notes { get; set; } // Any additional information about the prescription  
}  