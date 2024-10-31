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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly ILogger<AccountService> _logger;

        public DoctorService(IDoctorRepository doctorRepository, IMedicineRepository medicineRepository, IPrescriptionRepository prescriptionRepository, ILogger<AccountService> logger)
        {
            _doctorRepository = doctorRepository;
            _medicineRepository = medicineRepository;
            _prescriptionRepository = prescriptionRepository;
            _logger = logger;
        }

        public async Task<DoctorInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository
                .Get()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId), cancellationToken);
            var doctorResponse = new DoctorInfoResponseDto();

            if (doctor != null)
            {
                doctorResponse.RegistrationId = doctor.RegistrationId;
                doctorResponse.FirstName = doctor.User.FirstName;
                doctorResponse.LastName = doctor.User.LastName;
                doctorResponse.Speciality = "Otorrinolaringologo";
            }

            return doctorResponse;
        }

        public async Task<List<MedicineInfoResponseDto>> SearchMedicinesAsync(string medicineName, CancellationToken cancellationToken)
        {
            var medicines = await _medicineRepository
                .Get().Where(x => x.CommercialName.StartsWith(medicineName))
                .ToListAsync(cancellationToken);
            
            var medicineList = new List<MedicineInfoResponseDto>();

            foreach (var medicine in medicines)
            {
                var medicineDto = new MedicineInfoResponseDto
                {
                    MedicineId = medicine.MedicineId,
                    CommercialName = medicine.CommercialName,
                    Concentration = medicine.Concentration,
                    Presentation = medicine.Presentation
                };

                medicineList.Add(medicineDto);
            }

            return medicineList;
        }


        /// <inheritdoc />
        public async Task<string> CreatePrescriptionAsync(CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                var lastPrescription = 
                    await _prescriptionRepository
                        .Get()
                        .OrderByDescending(x => x.PrescriptionCode)
                        .FirstOrDefaultAsync(cancellationToken);

                string newCode;

                if (lastPrescription == null)
                {
                    newCode = "AA-000";
                } else
                {
                    newCode = GetNextCode(lastPrescription.PrescriptionCode);
                }

                var prescription = new Prescription
                {
                    PrescriptionCode = newCode,
                    RegistrationId = requestDto.DoctorRegistration,
                    PatientId = requestDto.PatientId,
                    MedicineId = requestDto.MedicineId,
                    Diagnostic = requestDto.Diagnostic,
                    Indications = requestDto.Indications,
                    CreatedDate = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddDays(90),
                    StatusId = (int)StatusEnum.Emitida
                };

                await _prescriptionRepository.AddAsync(prescription, cancellationToken);
                await _prescriptionRepository.SaveChangesAsync(cancellationToken);

                return prescription.PrescriptionCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletePrescriptionAsync(int prescriptionId, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId, cancellationToken);
            
            if (prescription == null)
            {
                return;
            }

            _prescriptionRepository.Delete(prescription);
            await _prescriptionRepository.SaveChangesAsync(cancellationToken);
        }

        public string GetNextCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return "AA-000";

            // Separar el código en letras y número
            string letters = code.Substring(0, 2);
            int number = int.Parse(code.Substring(3));

            // Incrementar el número
            number++;

            if (number > 999) // Si el número supera el límite, reiniciamos a 000 y aumentamos las letras
            {
                number = 0;
                letters = IncrementLetters(letters);
            }

            // Formatear el código de nuevo en el formato "AA-000"
            return $"{letters}-{number:D3}";
        }

        private string IncrementLetters(string letters)
        {
            char firstLetter = letters[0];
            char secondLetter = letters[1];

            if (secondLetter < 'Z') // Incrementar la segunda letra
            {
                secondLetter++;
            }
            else // Si la segunda letra es 'Z', reiniciar a 'A' y subir la primera letra
            {
                secondLetter = 'A';

                if (firstLetter < 'Z')
                {
                    firstLetter++;
                }
                else // Si las letras alcanzan "ZZ", se ha llegado al máximo
                {
                    throw new InvalidOperationException("Se ha alcanzado el código máximo: ZZ-999.");
                }
            }

            return $"{firstLetter}{secondLetter}";
        }
    }
}
