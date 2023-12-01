namespace DocService.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public string Post { get; set; }
        public string SpecName { get; set; }
        public int UserId { get; set; }
        
    }
}
