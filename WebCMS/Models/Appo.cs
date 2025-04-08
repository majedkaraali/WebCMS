namespace WebCMS.Models
{
    namespace WebCMS.Models
    {
        public class Appointment
        {
            public int Id { get; set; }


            public DateTime AppointmentDate { get; set; }

            public string Status { get; set; } // Scheduled, Completed, Canceled
        }

    }

}
