using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string? AppointmentDateSlot { get; set; }
        public string? AppointmentTimeSlot { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? ContactEmailAddress { get; set; }
        public string? ContactWhatsappNumber { get; set; }
        
        // Foreign Keys
        public Guid? EntityId { get; set; }  // Patient reference
        public Guid? LocationId { get; set; }

        // Navigation Properties
        public virtual Location? Location { get; set; }
    }
}
