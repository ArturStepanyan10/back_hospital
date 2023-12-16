namespace CompositeService.Models
{
    public class SheduleCos
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Date { get; set; }
        public List<int> AdmissionId { get; set; }


    }
}
