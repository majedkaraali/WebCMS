namespace WebCMS.Models
{
    public class PatiensDiseases
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DiseaseId { get; set; }
        public string? DiseaseName { get; set; }
        public string? ICD10Code { get; set; }
        public string? DiagnosedbyDr { get; set; }
        public DateTime? DiagnosedDate { get; set; }
        public bool? IsActive { get; set; } = true;

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }


    }
}
