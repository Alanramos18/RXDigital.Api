using RXDigital.Api.Entities;

namespace RXDigital.Api.Repositories.Interfaces
{
    public interface IPrescriptionRepository : IBaseRepository<Prescription>
    {
        Task<List<GetFilteredRxProc>> GetPrescriptionsAsync(string from, string to, CancellationToken cancellationToken);
        Task<List<GetTopRxProc>> GetTopRxAsync(string top, CancellationToken cancellationToken);
        Task<List<GetTopMedicsProc>> GetTopMedicsAsync(string top, CancellationToken cancellationToken);
    }
}
