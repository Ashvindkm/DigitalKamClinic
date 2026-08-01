using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class User : Account
    {
        public Guid TenantId { get; set; }

        [JsonIgnore]
        public Tenant? Tenant { get; set; }
    }
}
