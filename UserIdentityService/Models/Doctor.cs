using Microsoft.AspNetCore.Identity;

namespace UserIdentityService.Models
{
    public class Doctor : IdentityUser
    {
        public int DoctorId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Experience { get; set; }
        public string Post { get; set; }
        public string SpecName { get; set; }
        public string Role { get; set; }
    }
}
