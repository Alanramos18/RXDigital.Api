using RXDigital.Api.Entities;

namespace RXDigital.Api.Repositories.Interfaces
{
    public interface IPharmaceuticalRepository : IBaseRepository<Pharmaceutical>
    {
        Task<GetPrescriptionsPharmaceuticalProc> GetPrescriptionsInfoAsync(string code, CancellationToken cancellationToken);
    }
}
