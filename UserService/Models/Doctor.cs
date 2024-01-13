namespace UserService.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public string Post { get; set; }
        public string SpecName { get; set; }
        // Внешний ключ для связи с пользователем
        public string Username { get; set; }
        public User User { get; set; }
    }
}
