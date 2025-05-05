using System.ComponentModel.DataAnnotations;

namespace WebCMS.Models
{
    public class LabTestCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
