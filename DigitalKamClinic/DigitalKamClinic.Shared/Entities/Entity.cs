using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }

        public Guid? ConfigEntityId { get; set; }
        public string? EntityName { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
