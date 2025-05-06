namespace WebCMS.Models
{
    public class PatientAllergy
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        public DateTime? DateDiagnosed { get; set; }
        public string? Notes { get; set; }
    }

}
