using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }
}