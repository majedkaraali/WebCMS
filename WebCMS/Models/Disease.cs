namespace WebCMS.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Disease
    {
        public int Id { get; set; }

        public string? ICD10Code { get; set; }

        public string? DiseaseCode { get; set; }

        [MaxLength(255)] // This maps to NVARCHAR(255)
        public string? DiseaseName { get; set; }

        public string? Description { get; set; }

        public string? DiseaseType { get; set; }
    }


}
