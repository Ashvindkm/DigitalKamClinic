using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.DTOs
{
    public class TenantDTO
    {
        public Guid Id { get; set; }
        public string? EnterpriseName { get; set; }
        public int? Status { get; set; }
        public bool? IsRoot { get; set; } = false;

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
