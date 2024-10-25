using RXDigital.Api.Entities;

namespace RXDigital.Api.Repositories.Interfaces
{
    public interface IPrescriptionRepository : IBaseRepository<Prescription>
    {
        //Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId);
    }
}
