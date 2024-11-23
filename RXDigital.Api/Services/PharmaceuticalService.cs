using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Helpers;
using RXDigital.Api.Repositories;
using RXDigital.Api.Repositories.Interfaces;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Services
{
    public class PharmaceuticalService : IPharmaceuticalService
    {
        private readonly IPharmaceuticalRepository _pharmaceuticalRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PharmaceuticalService(IPharmaceuticalRepository pharmaceuticalRepository, IPrescriptionRepository prescriptionRepository, ILogger<AccountService> logger)
        {
            _pharmaceuticalRepository = pharmaceuticalRepository;
            _prescriptionRepository = prescriptionRepository;
            _logger = logger;
        }

        public async Task<PharmaceuticInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var pharma = await _pharmaceuticalRepository
                .Get()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.AccountId.Equals(userId), cancellationToken);

            var pharmaResponse = new PharmaceuticInfoResponseDto();

            if (pharma != null)
            {
                pharmaResponse.RegistrationId = pharma.RegistrationId;
                pharmaResponse.FirstName = pharma.Account.FirstName;
                pharmaResponse.LastName = pharma.Account.LastName;
            }

            return pharmaResponse;
        }

        /// <inheritdoc />
        public async Task<GetPrescriptionsPharmaceuticalProc> GetPrescriptionInformationAsync(string code, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _pharmaceuticalRepository.GetPrescriptionsInfoAsync(code, cancellationToken);

                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AcceptRxAsync(string code, int matricula, CancellationToken cancellationToken)
        {
            try
            { 
                var rx = await _prescriptionRepository.Get().FirstOrDefaultAsync(x => x.PrescriptionCode.Equals(code), cancellationToken);

                if (rx == null)
                {
                    return;
                }

                rx.StatusId = 3;
                rx.PharmaceuticalRegistrationId = matricula;

                _prescriptionRepository.Update(rx);
                await _prescriptionRepository.SaveChangesAsync(cancellationToken);
                
                return;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RejectRxAsync(RejectRxResquestDto rejectDto, CancellationToken cancellationToken)
        {
            try
            {
                var rx = await _prescriptionRepository.Get().FirstOrDefaultAsync(x => x.PrescriptionCode.Equals(rejectDto.Codigo), cancellationToken);

                if (rx == null)
                {
                    return;
                }

                rx.StatusId = 4;
                rx.MotivoRechazo = rejectDto.MotivoRechazo;
                rx.PharmaceuticalRegistrationId = rejectDto.Matricula;

                _prescriptionRepository.Update(rx);
                await _prescriptionRepository.SaveChangesAsync(cancellationToken);

                return;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
