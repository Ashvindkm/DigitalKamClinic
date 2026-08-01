using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class EntityDetail
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? EntityId { get; set; }
        public Guid? ConfigEntityDetailId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public int? InputType { get; set; }
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public bool? Display { get; set; }
        public string? Validation { get; set; }
    }
}
