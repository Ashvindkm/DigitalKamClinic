using System;

namespace DigitalKamClinic.Shared.Entities
{
    /// <summary>
    /// Patient entity for the dental clinic system
    /// Currently uses Entity table for storage, this is a wrapper/view model
    /// </summary>
    public class Patient
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }

        // Basic Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        // Contact Information
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }

        // Medical Information
        public string? Allergies { get; set; }
        public string? MedicalConditions { get; set; }
        public string? Medications { get; set; }

        // Emergency Contact
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        // Notes
        public string? Notes { get; set; }

        // Status
        public bool IsActive { get; set; } = true;

        // Audit
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
