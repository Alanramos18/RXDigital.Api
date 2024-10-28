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
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<AccountService> _logger;

        public PatientService(IPatientRepository patientRepository, ISocialWorkRepository socialWorkRepository, ILocationRepository locationRepository, ILogger<AccountService> logger)
        {
            _patientRepository = patientRepository;
            _socialWorkRepository = socialWorkRepository;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<PatientInfoResponseDto> GetBasicInformationAsync(int patientId, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository
                    .Get()
                    .Include(x => x.SocialWork)
                    .Include(x => x.Location)
                    .FirstOrDefaultAsync(x => x.PatientId == patientId, cancellationToken);

                var patientInfo = patient.Convert();

                return patientInfo;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<int> CreatePatientAsync(PatientResquestDto patientResquestDto, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                Name = patientResquestDto.Location,
                Province = patientResquestDto.Province,
                Country = patientResquestDto.Country
            };

            await _locationRepository.AddAsync(location, cancellationToken);

            var patient = new Patient
            {
                PatientId = patientResquestDto.Id,
                FirstName = patientResquestDto.FirstName,
                LastName = patientResquestDto.LastName,
                BirthDay = patientResquestDto.BirthDay,
                Gender = patientResquestDto.Gender,
                Cellphone = patientResquestDto.Cellphone,
                HomePhone = patientResquestDto.HomePhone,
                Email = patientResquestDto.Email,
                SocialWorkId = patientResquestDto.SocialWorkId,
                SocialNumber = patientResquestDto.SocialNumber,
                IsAvailable = true,
                AddressStreet = patientResquestDto.AddressStreet,
                AddressNumber = patientResquestDto.AddressNumber,
                LocationId = location.LocationId
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
