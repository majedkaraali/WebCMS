namespace WebCMS.Models
{
    public class LabOrder

    {
        public int Id { get; set; }
        public string LabTestName { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




    }
}
