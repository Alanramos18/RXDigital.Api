using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Entities;
using RXDigital.Api.Entities.Mappings;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Context
{
    public class RxDigitalContext : IdentityDbContext<AccountEntity>, IRxDigitalContext
    {
        public RxDigitalContext() : base() { }
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
        public DbSet<GetPrescriptionsProc> GetPrescriptionsProcs { get; set; }
        public DbSet<GetFilteredRxProc> GetFilteredRxProc { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GetPrescriptionsProc>().HasNoKey();
            builder.Entity<GetPrescriptionsProc>().Property(t => t.CodigoReceta)
                .HasColumnName("CodigoReceta");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.ExpirationDate)
                .HasColumnName("Fecha de Vencimiento");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.Diagnostic)
                .HasColumnName("diagnostico");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.MedicineName)
                .HasColumnName("nombre_comercial");
            builder.Entity<GetPrescriptionsProc>().Property(t => t.Concentration)
                .HasColumnName("concentracion");

            builder.Entity<GetFilteredRxProc>().HasNoKey();
            builder.Entity<GetFilteredRxProc>().Property(t => t.CodigoReceta)
                .HasColumnName("Codigo Receta");
            builder.Entity<GetFilteredRxProc>().Property(t => t.FechaEmision)
                .HasColumnName("Fecha Emision");
            builder.Entity<GetFilteredRxProc>().Property(t => t.PatientName)
                .HasColumnName("Nombre del paciente");
            builder.Entity<GetFilteredRxProc>().Property(t => t.MedicineName)
                .HasColumnName("Recetado por");
            builder.Entity<GetFilteredRxProc>().Property(t => t.Estado)
                .HasColumnName("Estado de Receta");

            builder.Entity<GetTopRxProc>().HasNoKey();
            builder.Entity<GetTopRxProc>().Property(t => t.NombreComercial)
                .HasColumnName("nombre_comercial");
            builder.Entity<GetTopRxProc>().Property(t => t.Presentacion)
                .HasColumnName("presentacion");
            builder.Entity<GetTopRxProc>().Property(t => t.Concentracion)
                .HasColumnName("concentracion");
            builder.Entity<GetTopRxProc>().Property(t => t.TotalRecetado)
                .HasColumnName("Total Recetado");

            builder.Entity<GetTopMedicsProc>().HasNoKey();
            builder.Entity<GetTopMedicsProc>().Property(t => t.Matricula)
                .HasColumnName("matricula");
            builder.Entity<GetTopMedicsProc>().Property(t => t.NombreMedico)
                .HasColumnName("Nombre Medico");
            builder.Entity<GetTopMedicsProc>().Property(t => t.CantidadReceta)
                .HasColumnName("Total Recetas");
            

            //builder.Entity<RxInfo>().HasNoKey();
            //builder.Entity<RxInfo>().Property(t => t.NombrePaciente)
            //    .HasColumnName("Nombre Paciente");
            //builder.Entity<RxInfo>().Property(t => t.Dni)
            //    .HasColumnName("dni");
            //builder.Entity<RxInfo>().Property(t => t.ObraSocial)
            //    .HasColumnName("Obra Social");
            //builder.Entity<RxInfo>().Property(t => t.PlanSocial)
            //    .HasColumnName("plan_social");
            //builder.Entity<RxInfo>().Property(t => t.NumeroAfiliado)
            //    .HasColumnName("numero_afiliado");
            //builder.Entity<RxInfo>().Property(t => t.NombreMedico)
            //    .HasColumnName("Nombre Medico");
            //builder.Entity<RxInfo>().Property(t => t.Matricula)
            //    .HasColumnName("matricula");
            //builder.Entity<RxInfo>().Property(t => t.Diagnostico)
            //    .HasColumnName("diagnostico");
            //builder.Entity<RxInfo>().Property(t => t.Indicaciones)
            //    .HasColumnName("indicaciones");
            //builder.Entity<RxInfo>().Property(t => t.Expiracion)
            //    .HasColumnName("expiracion");
            //builder.Entity<RxInfo>().Property(t => t.IdEstado)
            //    .HasColumnName("id_estado");

            //builder.Entity<MedicineInfo>().HasNoKey();
            //builder.Entity<MedicineInfo>().Property(t => t.NombreComercial)
            //    .HasColumnName("nombre_comercial");
            //builder.Entity<MedicineInfo>().Property(t => t.Presentacion)
            //    .HasColumnName("presentacion");
            //builder.Entity<MedicineInfo>().Property(t => t.Concentracion)
            //    .HasColumnName("concentracion");
            //builder.Entity<MedicineInfo>().Property(t => t.Indicaciones)
            //    .HasColumnName("indicaciones");

            //builder.Entity<GetPrescriptionsPharmaceuticalProc>().HasNoKey();
            //builder.Entity<GetPrescriptionsPharmaceuticalProc>()
            //    .HasOne(x => x.RxInfo)
            //    .WithOne()
            //    .HasForeignKey<RxInfo>(x => x.Id);
            //builder.Entity<GetPrescriptionsPharmaceuticalProc>()
            //    .HasMany(x => x.MedicineList)
            //    .WithOne()
            //    .HasForeignKey(x => x.Id);


            base.OnModelCreating(builder);
            //SeedRoles(builder);

            builder.ApplyConfiguration(new EspecialidadEntityMapping());
            builder.ApplyConfiguration(new AccountEntityMapping());
            builder.ApplyConfiguration(new DoctorEntityMapping());
            builder.ApplyConfiguration(new MedicineEntityMapping());
            builder.ApplyConfiguration(new PatientEntityMapping());
            builder.ApplyConfiguration(new PharmaceuticalEntityMapping());
            builder.ApplyConfiguration(new PrescriptionEntityMapping());
            builder.ApplyConfiguration(new RoleEntityMapping());
            builder.ApplyConfiguration(new SocialWorkEntityMapping());
            builder.ApplyConfiguration(new StatusEntityMapping());
            builder.ApplyConfiguration(new PrescriptionMedicineEntityMapping());
        }

        //private static void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //            new IdentityRole { Name = "Unlam", NormalizedName = "Unlam", ConcurrencyStamp = "1" }
        //        );
        //}
    }
}
