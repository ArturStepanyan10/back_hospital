using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Models
{
    public class Patient : IdentityUser
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role {  get; set; }
    }
}
