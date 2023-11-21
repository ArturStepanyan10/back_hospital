using System.Numerics;

namespace ServiceMicService.Model
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int SpecializationId { get; set; }
        public int DoctorId { get; set; }
        public string Time { get; set; }
        public int CostService { get; set; }
    }
}
