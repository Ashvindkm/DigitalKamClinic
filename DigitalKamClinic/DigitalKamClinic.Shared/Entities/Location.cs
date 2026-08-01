using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? AddressId { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
