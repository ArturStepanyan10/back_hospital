namespace SheduleService.Models
{
    public class Shedule
    {
        public int SheduleId { get; set; }
        public int DoctorId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int AdmissionId { get; set; }

    }
}
