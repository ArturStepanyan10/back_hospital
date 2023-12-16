namespace AdmissionService.Models
{
    public class Admission
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int Office { get; set; }
        public string Time { get; set; }
    }
}
