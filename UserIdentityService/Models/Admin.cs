using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Models
{
    public class Admin : IdentityUser
    {
        public int AdminId { get; set; }
        public string Role { get; set; }
    }
}
