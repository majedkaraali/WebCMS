namespace WebCMS.Models
{
    public class LabOrder

    {
        public int Id { get; set; }
        
        public int LabTestId { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public string Status { get; set; }

        public LabTest Test { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




    }
}
