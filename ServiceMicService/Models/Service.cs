namespace ServiceMicService.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int SpecializationId { get; set; }
        public int DoctorId { get; set; }
        public int CostService { get; set; }
    }
}
