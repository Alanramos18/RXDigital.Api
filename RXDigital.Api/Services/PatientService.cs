using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
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
            try
            {
                var patient = await _patientRepository
                    .Get()
                    .Include(x => x.SocialWork)
                    .FirstOrDefaultAsync(x => x.Dni == patientId, cancellationToken);

                var patientInfo = patient.Convert();

                return patientInfo;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public async Task UpdatePatientAsync(int patientId, PatientResquestDto patientResquestDto, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
                patient.Dni = patientResquestDto.Dni;
                patient.Nombre = patientResquestDto.Nombre;
                patient.Apellido = patientResquestDto.Apellido;
                patient.FechaNacimiento = patientResquestDto.FechaNacimiento;
                patient.FechaInscripcion = DateTime.UtcNow;
                patient.Genero = patientResquestDto.Genero;
                patient.Celular = patientResquestDto.Celular;
                patient.Telefono = patientResquestDto.Telefono;
                patient.Email = patientResquestDto.Email;
                patient.SocialWorkId = patientResquestDto.ObraSocialId;
                patient.NumeroAfiliado = patientResquestDto.NumeroAfiliado;
                patient.Habilitado = true;
                patient.Domicilio = patientResquestDto.Direccion;
                patient.Localidad = patientResquestDto.Localidad;
                patient.Provincia = patientResquestDto.Provincia;
                patient.Nacionalidad = patientResquestDto.Nacionalidad;

                _patientRepository.Update(patient);
                await _patientRepository.SaveChangesAsync(cancellationToken);

                return;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> CreatePatientAsync(PatientResquestDto patientResquestDto, CancellationToken cancellationToken)
        {
            try
            {
                var patient = new Patient
                {
                    Dni = patientResquestDto.Dni,
                    Nombre = patientResquestDto.Nombre,
                    Apellido = patientResquestDto.Apellido,
                    FechaNacimiento = patientResquestDto.FechaNacimiento,
                    FechaInscripcion = DateTime.UtcNow,
                    Genero = patientResquestDto.Genero,
                    Celular = patientResquestDto.Celular,
                    Telefono = patientResquestDto.Telefono,
                    Email = patientResquestDto.Email,
                    SocialWorkId = patientResquestDto.ObraSocialId,
                    NumeroAfiliado = patientResquestDto.NumeroAfiliado,
                    Habilitado = true,
                    Domicilio = patientResquestDto.Direccion,
                    Localidad = patientResquestDto.Localidad,
                    Provincia = patientResquestDto.Provincia,
                    Nacionalidad = patientResquestDto.Nacionalidad
                };

                await _patientRepository.AddAsync(patient, cancellationToken);
                await _patientRepository.SaveChangesAsync(cancellationToken);

                return patient.Dni;
            }
            catch (Exception e)
            {
                throw e;
            }
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

        public async Task DeletePatientAsync(int patientId, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);

                if (patient == null)
                {
                    return;
                }

                // NO borramos, solo deshabilitamos
                //_patientRepository.Delete(patient);
                patient.Habilitado = !patient.Habilitado;
                _patientRepository.Update(patient);
                await _patientRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
