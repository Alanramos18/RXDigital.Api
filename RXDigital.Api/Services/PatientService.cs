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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ISocialWorkRepository _socialWorkRepository;
        private readonly ILogger<AccountService> _logger;

        public PatientService(IPatientRepository patientRepository, ISocialWorkRepository socialWorkRepository, ILogger<AccountService> logger)
        {
            _patientRepository = patientRepository;
            _socialWorkRepository = socialWorkRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<PatientInfoResponseDto> GetBasicInformationAsync(int patientId, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository
                .Get()
                .Include(x => x.SocialWork)
                .FirstOrDefaultAsync(x => x.PatientId == patientId, cancellationToken);

            var patientInfo = patient.Convert();

            return patientInfo;
        }

        public async Task<int> CreatePatientAsync(PatientResquestDto patientResquestDto, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                PatientId = patientResquestDto.Id,
                FirstName = patientResquestDto.FirstName,
                LastName = patientResquestDto.LastName,
                BirthDay = patientResquestDto.BirthDay,
                Gender = patientResquestDto.Gender,
                Nationality = patientResquestDto.Nationality,
                //Address = patientResquestDto.Address,
                Cellphone = patientResquestDto.Cellphone,
                HomePhone = patientResquestDto.HomePhone,
                //Email = patientResquestDto.Email,
                SocialWorkId = patientResquestDto.SocialWorkId,
                SocialNumber = patientResquestDto.SocialNumber,
                //AvalabilityStatus = "Habilitado"
            };

            await _patientRepository.AddAsync(patient, cancellationToken);
            await _patientRepository.SaveChangesAsync(cancellationToken);

            return patient.PatientId;
        }

        public async Task<List<SocialWorksInfoResponseDto>> GetSocialWorksAsync(CancellationToken cancellationToken)
        {
            var socialWorks = await _socialWorkRepository.Get().ToListAsync(cancellationToken);

            var socialWorksList = new List<SocialWorksInfoResponseDto>();

            foreach (var socialWork in socialWorks)
            {
                var dto = new SocialWorksInfoResponseDto
                {
                    SocialWorkId = socialWork.SocialWorkId,
                    Name = socialWork.Name,
                    SocialPlan = socialWork.SocialPlan
                };

                socialWorksList.Add(dto);
            }

            return socialWorksList;
        }

        public async Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId, CancellationToken cancellationToken)
        {
            var prescriptions = await _patientRepository.GetPrescriptionsAsync(patientId);

            return prescriptions;
        }
    }
}
