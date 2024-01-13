using Microsoft.AspNetCore.Identity;

namespace UserService.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        // Свойство навигации для связи с данными пациента
        public Patient Patient { get; set; }

        // Свойство навигации для связи с данными врача
        public Doctor Doctor { get; set; }

        // Свойство навигации для связи с данными админа
        public Admin Admin { get; set; }
        public string Username { get; internal set; }
    }
}
