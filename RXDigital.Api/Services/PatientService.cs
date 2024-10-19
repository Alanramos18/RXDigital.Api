using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RXDigital.Api.DTOs;
using RXDigital.Api.Helpers;
using RXDigital.Api.Repositories;
using RXDigital.Api.Repositories.Interfaces;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<AccountService> _logger;

        public PatientService(IPatientRepository patientRepository, ILogger<AccountService> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<PatientInfoResponseDto> GetBasicInformationAsync(int patientId, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);

            var patientInfo = patient.Convert();

            return patientInfo;
        }

        public async Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId, CancellationToken cancellationToken)
        {
            var prescriptions = await _patientRepository.GetPrescriptionsAsync(patientId);

            return prescriptions;
        }
    }
}
