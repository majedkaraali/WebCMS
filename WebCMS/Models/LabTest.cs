using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models
{
    public class LabTest
    {
        [Key]
        public int Id { get; set; }
        public string TestName { get; set; }
        public string? Unit { get; set; }

        public double? MinRange { get; set; }
        public double? MaxRange { get; set; }

        public string? sex { get; set; } // Male - Female - Both

        public string? NormalValue { get; set; }

        public int? CategoryId { get; set; }

        public LabTestCategory? TestCategory { get; set; }
    }
}

