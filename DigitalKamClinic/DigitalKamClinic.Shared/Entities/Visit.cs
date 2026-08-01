using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? WorkTypeId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? SubLocationId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? Status { get; set; }
    }
}
