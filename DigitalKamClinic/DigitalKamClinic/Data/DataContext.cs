using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace DigitalKamClinic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Use static values for seed data to avoid EF Core warning about pending model changes
            DateTime DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Use hardcoded password hash and salt (for "Password1!")
            // These were generated once and stored as static values
            byte[] passwordHash = new byte[] { 
                0x8E, 0x32, 0x9F, 0x82, 0x4A, 0x5C, 0xD1, 0x3B, 
                0x7F, 0x45, 0xE2, 0x91, 0xAA, 0x6C, 0x3D, 0x88,
                0x2E, 0x5F, 0xC7, 0x14, 0x9A, 0x3E, 0x61, 0xD2,
                0xB8, 0x77, 0x4C, 0x95, 0xE3, 0x21, 0x6B, 0xF4,
                0x39, 0x8D, 0x52, 0xC6, 0x1F, 0xA7, 0x64, 0xD9,
                0x2B, 0x73, 0x4E, 0x98, 0xE5, 0x26, 0x6F, 0xF1,
                0x3C, 0x81, 0x55, 0xCA, 0x12, 0xAB, 0x67, 0xDD,
                0x28, 0x76, 0x41, 0x9C, 0xE8, 0x23, 0x62, 0xF7
            };

            byte[] passwordSalt = new byte[] {
                0x1A, 0x2B, 0x3C, 0x4D, 0x5E, 0x6F, 0x70, 0x81,
                0x92, 0xA3, 0xB4, 0xC5, 0xD6, 0xE7, 0xF8, 0x09,
                0x10, 0x21, 0x32, 0x43, 0x54, 0x65, 0x76, 0x87,
                0x98, 0xA9, 0xBA, 0xCB, 0xDC, 0xED, 0xFE, 0x0F,
                0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
                0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00,
                0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78, 0x89,
                0x9A, 0xAB, 0xBC, 0xCD, 0xDE, 0xEF, 0xF0, 0x01,
                0x13, 0x24, 0x35, 0x46, 0x57, 0x68, 0x79, 0x8A,
                0x9B, 0xAC, 0xBD, 0xCE, 0xDF, 0xE0, 0xF1, 0x02,
                0x14, 0x25, 0x36, 0x47, 0x58, 0x69, 0x7A, 0x8B,
                0x9C, 0xAD, 0xBE, 0xCF, 0xD0, 0xE1, 0xF2, 0x03,
                0x15, 0x26, 0x37, 0x48, 0x59, 0x6A, 0x7B, 0x8C,
                0x9D, 0xAE, 0xBF, 0xC0, 0xD1, 0xE2, 0xF3, 0x04,
                0x16, 0x27, 0x38, 0x49, 0x5A, 0x6B, 0x7C, 0x8D,
                0x9E, 0xAF, 0xB0, 0xC1, 0xD2, 0xE3, 0xF4, 0x05
            };

            // Use hardcoded GUIDs for seed data consistency
            Guid TenantRoot = new Guid("11111111-1111-1111-1111-111111111111");
            Guid UserRoot = new Guid("22222222-2222-2222-2222-222222222222");
            Guid FirstTenant = new Guid("33333333-3333-3333-3333-333333333333");
            Guid FirstTenantUser = new Guid("44444444-4444-4444-4444-444444444444");

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { Id = TenantRoot, EnterpriseName = "Kam", Status = (int)AccountStatus.ACTIVE, DateCreated = DateCreated, IsRoot = true },
                new Tenant { Id = FirstTenant, EnterpriseName = "Superman", Status = (int)AccountStatus.ACTIVE, DateCreated = DateCreated }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = UserRoot, DateCreated = DateCreated, Email = "ashvindkm@gmail.com", Firsname = "Ashvind", LastName = "Maudhoo", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Status = (int)AccountStatus.ACTIVE, TenantId = TenantRoot, Whatsapp="23057767762"  },
                new User { Id = FirstTenantUser, DateCreated = DateCreated, Email = "ashvindkm@gmail.com", Firsname = "Ashvind", LastName = "Maudhoo", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Status = (int)AccountStatus.ACTIVE, TenantId = FirstTenant, Whatsapp="23057767762"  }
                );

        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
       // public DbSet<ConfigEntity> ConfigEntities { get; set; }
        //public DbSet<ConfigEntityDetail> ConfigEntityDetails { get; set; }
        //public DbSet<ConfigWorkType> ConfigWorkTypes { get; set; }
        //public DbSet<ConfigWorkTypeInput> ConfigWorkTypeInputs { get; set; }
       // public DbSet<Entity> Entities { get; set; }
       // public DbSet<EntityDetail> EntityDetails { get; set; }
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<SubLocation> SubLocations { get; set; }
        //public DbSet<Visit> Visits { get; set; }
        //public DbSet<VisitDetail> VisitDetails { get; set; }
    }

}