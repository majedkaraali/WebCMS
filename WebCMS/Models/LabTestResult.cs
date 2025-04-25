namespace WebCMS.Models
{
    public class LabTestResult
    {
        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public string Result { get; set; }
        public string Remark { get; set; } // High, Low, Normal
        public string? Interpretation { get; set; }

        public LabOrder LapOrder { get; set; }
        public LabTest LabTest { get; set; }
    }
}
