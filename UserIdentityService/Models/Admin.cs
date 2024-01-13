using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Models
{
    public class Admin : IdentityUser
    {
        public int AdminId { get; set; }
    }
}
