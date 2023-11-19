﻿namespace SheduleService.Models
{
    public class Shedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Time { get; set; }
        public int AdmissionId { get; set; }

    }
}
