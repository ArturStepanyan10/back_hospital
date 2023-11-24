﻿

namespace CompositeService.Models
{
    public class PatientComp
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public int DoctorId { get; set; }
    }
}
