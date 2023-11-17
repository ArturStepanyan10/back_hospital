namespace MedicalReportService.Models
{
    public class MedicalReport
    {
        public int MedicalReportId { get; set; }
        public int PatientId { get; set; }
        public int AdmissionId { get; set; }
        public string Report {  get; set; }
    }
}
