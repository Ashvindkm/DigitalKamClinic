namespace DigitalKamClinic.Shared.DTOs
{
    public class AppointmentCreateDTO
    {
        public Guid PatientId { get; set; }
        public Guid LocationId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; } = string.Empty;
        public string? ReasonForVisit { get; set; }
    }
}
