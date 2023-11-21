namespace CompositeService.Models
{
    public class AdmissionCos
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int SheduleId { get; set; }
        public int Office { get; set; }
        public int ServiceId { get; set; }
        public string Time { get; set; }
    }
}
