namespace CompositeService.Models
{
    public class SheduleCos
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Time { get; set; }
        public int AdmissionId { get; set; }
    }
}
