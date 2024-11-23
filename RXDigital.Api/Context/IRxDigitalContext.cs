using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Context
{
    public interface IRxDigitalContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
        /// <summary>
        ///     Accounts entities.
        /// </summary>
        DbSet<AccountEntity> Accounts { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Medicine> Medicines { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Pharmaceutical> Pharmaceuticals { get; set; }
        DbSet<Prescription> Prescriptions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<SocialWork> SocialWorks { get; set; }
        DbSet<Status> Statuses { get; set; }
        DbSet<GetPrescriptionsProc> GetPrescriptionsProcs { get; set; }
    }
}
