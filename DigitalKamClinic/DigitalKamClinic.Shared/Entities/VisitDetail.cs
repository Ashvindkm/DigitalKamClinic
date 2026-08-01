using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Entities
{
    public class VisitDetail
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? VisitId { get; set; }
        public int? WorkTypeInputId { get; set; }
        public int? InputType { get; set; }
        public string? Label { get; set; }
        public string? Unit { get; set; }
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public string? Comments { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
