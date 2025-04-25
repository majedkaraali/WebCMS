using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models
{
    public class LabTest
    {
        [Key]
        public int Id { get; set; }
        public string TestName { get; set; }
    
        public string Unit { get; set; }
        public string? ReferenceRange { get; set; }
        public string? TestCategory { get; set; }
    }
}
