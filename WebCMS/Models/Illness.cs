namespace WebCMS.Models
{
    public class Illness
    {
        public int Id { get; set; }
        public string? IllnessName { get; set; }
        public string? IllnessType { get; set; }
        public string? Severity { get; set; }
        public string? Symptoms { get; set; }
        public string? Notes { get; set; }
        public string? DateDiagnosed { get; set; }

        public string? ICD10Code { get; set; } 
        public string? Description { get; set; }


        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
