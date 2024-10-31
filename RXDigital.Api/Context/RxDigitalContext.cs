using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Entities;
using RXDigital.Api.Entities.Mappings;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Context
{
    public class RxDigitalContext : IdentityDbContext<AccountEntity>, IRxDigitalContext
    {
        public RxDigitalContext(DbContextOptions<RxDigitalContext> options) : base(options) { }

        /// <inheritdoc />  
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmaceutical> Pharmaceuticals { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SocialWork> SocialWorks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<GetPrescriptionsProc> GetPrescriptionsProcs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GetPrescriptionsProc>().HasNoKey();
            builder.Entity<GetPrescriptionsProc>().Property(t => t.PrescriptionId)
                .HasColumnName("id_receta");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.ExpirationDate)
                .HasColumnName("Fecha de Vencimiento");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.Diagnostic)
                .HasColumnName("diagnostico");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.MedicineName)
                .HasColumnName("nombre_comercial");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.Concentration)
                .HasColumnName("concentracion");

            

            base.OnModelCreating(builder);
            //SeedRoles(builder);

            builder.ApplyConfiguration(new AccountEntityMapping());
            builder.ApplyConfiguration(new DoctorEntityMapping());
            builder.ApplyConfiguration(new MedicineEntityMapping());
            builder.ApplyConfiguration(new PatientEntityMapping());
            builder.ApplyConfiguration(new PharmaceuticalEntityMapping());
            builder.ApplyConfiguration(new PrescriptionEntityMapping());
            builder.ApplyConfiguration(new RoleEntityMapping());
            builder.ApplyConfiguration(new SocialWorkEntityMapping());
            builder.ApplyConfiguration(new StatusEntityMapping());
            builder.ApplyConfiguration(new LocationEntityMapping());
        }

        //private static void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //            new IdentityRole { Name = "Unlam", NormalizedName = "Unlam", ConcurrencyStamp = "1" }
        //        );
        //}
    }
}
