using System.Numerics;

namespace ServiceMicService.Model
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string DescriptionService { get; set; }
        public int CostService { get; set; }
    }
}
