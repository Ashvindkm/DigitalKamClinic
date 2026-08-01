using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class ConfigWorkTypeInput
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public int? WorkTypeId { get; set; }
        public int? InputType { get; set; }
        public string? Label { get; set; }
        public string? Unit { get; set; }
        public string? DefaultValue1 { get; set; }
        public string? DefaultValue2 { get; set; }
        public string? DefaultValue3 { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
