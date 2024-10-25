using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IPatientService
    {
        /// <summary>
        ///     Get patient info.
        /// </summary>
        /// <param name="patientId">Id of the patient</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Info of the patient</returns>
        Task<PatientInfoResponseDto> GetBasicInformationAsync(int patientId, CancellationToken cancellationToken);
        
        Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId, CancellationToken cancellationToken);
        Task<int> CreatePatientAsync(PatientResquestDto patientResquestDto, CancellationToken cancellationToken);
        Task<List<SocialWorksInfoResponseDto>> GetSocialWorksAsync(CancellationToken cancellationToken);
    }
}
