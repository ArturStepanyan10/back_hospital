using SheduleService.Models;

namespace CompositeService.Models
{
    public class CompositeDocShed
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int Experience { get; set; }
        public string Post { get; set; }
        public int SpecializationId { get; set; }
        public List<Shedule> Shedules { get; set; } = new List<Shedule>();
    }
}
