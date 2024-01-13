using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    // LoginViewModel.cs

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
