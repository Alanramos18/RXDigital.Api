using RXDigital.Api.Entities;

namespace RXDigital.Api.Repositories.Interfaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId);
    }
}
