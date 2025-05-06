namespace WebCMS.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        public string AllergyName { get; set; } = "";
        public string? AllergyType { get; set; }
        public string? Severity { get; set; }
        public string? Reaction { get; set; }
    }


}
