using DigitalKamClinic.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.DTOs
{
    public class UserDTO: AccountDTO
    {
        public Guid TenantId { get; set; }

        public TenantDTO Tenant { get; set; }
    }
}
