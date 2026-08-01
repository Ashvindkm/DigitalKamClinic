using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.DTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Firsname { get; set; }
        public string? LastName { get; set; }
        public string? Whatsapp { get; set; }
        public string? PasswordTemp { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? Status { get; set; }
    }
}
