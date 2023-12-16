namespace SheduleService.Models
{
    public class Shedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Date { get; set; }
        public List<int> AdmissionId { get; set; }

    }
}
