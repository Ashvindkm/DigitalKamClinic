using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using DigitalKamClinic.Shared.Entities;
using DigitalKamClinic.Shared.Helpers;
using DigitalKamClinic.Shared.Enums;

namespace DigitalKamClinic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // Suppress the PendingModelChangesWarning
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //DateTime DateCreated = new DateTime(2026, 8, 2, 0, 0, 0, DateTimeKind.Utc);
            DateTime DateCreated = DateTime.UtcNow;


            PasswordHelper.CreatePasswordHash("Password1!", out byte[] passwordHash, out byte[] passwordSalt);

            Guid TenantRoot = new Guid("9f24c6d1-7b38-4e5a-a912-3dcf8471b206");
            Guid UserRoot = new Guid("c18a72e4-056d-4b93-8f61-e2a7d539bc40");
            Guid FirstTenant = new Guid("4db9057f-a1c6-47e8-b324-69f0ca12de85");
            Guid FirstTenantUser = new Guid("e7631a90-2f4b-4cd7-95a8-b80136ef742c");

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
        public DbSet<ConfigEntity> ConfigEntities { get; set; }
        public DbSet<ConfigEntityDetail> ConfigEntityDetails { get; set; }
        public DbSet<ConfigWorkType> ConfigWorkTypes { get; set; }
        public DbSet<ConfigWorkTypeInput> ConfigWorkTypeInputs { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityDetail> EntityDetails { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<SubLocation> SubLocations { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitDetail> VisitDetails { get; set; }
    }

}