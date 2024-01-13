namespace UserService.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        // Внешний ключ для связи с пользователем
        public string Username { get; set; }
        public User User { get; set; }
    }
}
