using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IPharmaceuticalService
    {
        Task<GetPrescriptionsPharmaceuticalProc> GetPrescriptionInformationAsync(string code, CancellationToken cancellationToken);
        Task AcceptRxAsync(string code, int matricula, CancellationToken cancellationToken);
        Task RejectRxAsync(RejectRxResquestDto rejectDto, CancellationToken cancellationToken);
        Task<PharmaceuticInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}
