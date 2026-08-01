using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }
        public string? BlockApartment { get; set; }
        public string? StreetAddress { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public int? AddressType { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
