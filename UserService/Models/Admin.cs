namespace UserService.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        // Внешний ключ для связи с пользователем
        public string Username { get; set; }
        public User User { get; set; }
    }
}
