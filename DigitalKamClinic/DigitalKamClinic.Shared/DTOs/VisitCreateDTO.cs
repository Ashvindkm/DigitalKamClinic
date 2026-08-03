namespace DigitalKamClinic.Shared.DTOs
{
    public class VisitCreateDTO
    {
        public Guid? AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public Guid LocationId { get; set; }
        public DateTime VisitDate { get; set; }
        public string? ChiefComplaint { get; set; }
        public string TreatmentPerformed { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? ClinicalNotes { get; set; }
        public string? Prescription { get; set; }
        public string? NextVisitAdvice { get; set; }
        public decimal? Cost { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ToothNumbers { get; set; }
    }
}
