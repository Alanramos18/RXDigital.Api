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
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;

        public DoctorService(IDoctorRepository doctorRepository, IMedicineRepository medicineRepository,
            IPrescriptionMedicineRepository prescriptionMedicineRepository, IPrescriptionRepository prescriptionRepository, IAccountRepository accountRepository,
            IEmailService emailService, IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _prescriptionMedicineRepository = prescriptionMedicineRepository;
            _medicineRepository = medicineRepository;
            _prescriptionRepository = prescriptionRepository;
            _emailService = emailService;
            _patientRepository = patientRepository;
            _accountRepository = accountRepository;
        }

        public async Task<DoctorInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository
                .Get()
                .Include(x => x.User)
                .Include(x => x.Especialidad)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId), cancellationToken);
            var doctorResponse = new DoctorInfoResponseDto();

            if (doctor != null)
            {
                doctorResponse.RegistrationId = doctor.RegistrationId;
                doctorResponse.FirstName = doctor.User.FirstName;
                doctorResponse.LastName = doctor.User.LastName;
                doctorResponse.Speciality = doctor.Especialidad.Descripcion;
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
                    NombreComercial = medicine.CommercialName,
                    Concentracion = medicine.Concentration,
                    Presentacion = medicine.Presentation,
                    Descripcion = medicine.Description
                };

                medicineList.Add(medicineDto);
            }

            return medicineList;
        }

        public async Task UpdatePrescriptionAsync(string rxCode, CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                List<MedsDto> listaMeds = new List<MedsDto>();
                var rx = await _prescriptionRepository.Get().FirstOrDefaultAsync(x => x.PrescriptionCode.Equals(rxCode), cancellationToken);

                if (rx == null)
                {
                    throw new Exception("No existe!");
                }

                rx.Diagnostic = requestDto.Diagnostic;
                rx.Indications = requestDto.Indications;

                _prescriptionRepository.Update(rx);
                await _prescriptionRepository.SaveChangesAsync(cancellationToken);

                var existingMeds = await _prescriptionMedicineRepository.Get().Where(x => x.PrescriptionCode.Equals(rxCode)).ToListAsync(cancellationToken);
                foreach (var item in existingMeds)
                {
                    _prescriptionMedicineRepository.Delete(item);
                }

                foreach (var med in requestDto.Medicines)
                {                    
                    var msed = await _medicineRepository.Get().FirstOrDefaultAsync(x => x.MedicineId == med.MedicineId, cancellationToken);

                    listaMeds.Add(new MedsDto
                    {
                        CommercialName = msed.CommercialName,
                        Concentration = msed.Concentration,
                        Description = msed.Description,
                        Presentation = msed.Presentation,
                        Indications = med.Indications
                    });

                    var medicine = new PrescriptionMedicine
                    {
                        PrescriptionCode = rxCode,
                        MedicineId = med.MedicineId,
                        Indications = med.Indications
                    };

                    await _prescriptionMedicineRepository.AddAsync(medicine, cancellationToken);
                }

                await _prescriptionMedicineRepository.SaveChangesAsync(cancellationToken);

                var patient = await _patientRepository.Get().Include(x => x.SocialWork).FirstOrDefaultAsync(x => x.Dni == requestDto.PatientId, cancellationToken);
                var medic = await _doctorRepository.GetByIdAsync(requestDto.DoctorRegistration, cancellationToken);
                var doctor = await _doctorRepository
                    .Get()
                    .Include(x => x.User)
                    .Include(x => x.Especialidad)
                    .FirstOrDefaultAsync(x => x.RegistrationId.Equals(requestDto.DoctorRegistration), cancellationToken);

                AllInfo allInfo = new AllInfo
                {
                    Prescription = rx,
                    AccountEntity = doctor.User,
                    Patient = patient,
                    Doctor = medic,
                    Medicines = listaMeds
                };

                await _emailService.SendRxEmailAsync(patient.Email, allInfo, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<string> CreatePrescriptionAsync(CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                List<MedsDto> listaMeds = new List<MedsDto>();

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
                    Diagnostic = requestDto.Diagnostic,
                    Indications = requestDto.Indications,
                    CreatedDate = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddDays(90),
                    StatusId = (int)StatusEnum.Emitida
                };

                await _prescriptionRepository.AddAsync(prescription, cancellationToken);
                await _prescriptionRepository.SaveChangesAsync(cancellationToken);

                foreach (var medicineDto in requestDto.Medicines)
                {
                    var med = await _medicineRepository.Get().FirstOrDefaultAsync(x => x.MedicineId == medicineDto.MedicineId , cancellationToken);

                    listaMeds.Add(new MedsDto
                    {
                        CommercialName = med.CommercialName,
                        Concentration = med.Concentration,
                        Description = med.Description,
                        Presentation = med.Presentation,
                        Indications = medicineDto.Indications
                    });

                    var medicine = new PrescriptionMedicine
                    {
                        PrescriptionCode = newCode,
                        MedicineId = medicineDto.MedicineId,
                        Indications = medicineDto.Indications
                    };

                    await _prescriptionMedicineRepository.AddAsync(medicine, cancellationToken);
                }

                await _prescriptionMedicineRepository.SaveChangesAsync(cancellationToken);

                var patient = await _patientRepository.Get().Include(x => x.SocialWork).FirstOrDefaultAsync(x => x.Dni == requestDto.PatientId, cancellationToken);
                var medic = await _doctorRepository.GetByIdAsync(requestDto.DoctorRegistration, cancellationToken);
                var doctor = await _doctorRepository
                    .Get()
                    .Include(x => x.User)
                    .Include(x => x.Especialidad)
                    .FirstOrDefaultAsync(x => x.RegistrationId.Equals(requestDto.DoctorRegistration), cancellationToken);

                AllInfo allInfo = new AllInfo
                {
                    Prescription = prescription,
                    AccountEntity = doctor.User,
                    Patient = patient,
                    Doctor = medic,
                    Medicines = listaMeds
                };

                await _emailService.SendRxEmailAsync(patient.Email, allInfo, cancellationToken);

                return prescription.PrescriptionCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Prescription> GetPrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionRepository.Get().FirstOrDefaultAsync(x => x.PrescriptionCode.Equals(prescriptionCode), cancellationToken);

            return prescription;
        }

        public async Task DeletePrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionRepository.Get().FirstOrDefaultAsync(x => x.PrescriptionCode.Equals(prescriptionCode), cancellationToken);

            if (prescription == null)
            {
                return;
            }

            // NO BORRA
            //_prescriptionRepository.Delete(prescription);
            prescription.StatusId = 2;
            _prescriptionRepository.Update(prescription);
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

    public class AllInfo
    {
        public Prescription Prescription { get; set; }
        public List<MedsDto> Medicines { get; set; }
        public Patient Patient { get; set; }
        public AccountEntity AccountEntity { get; set; }
        public Doctor Doctor { get; set; }
    }

    public class MedsDto
    {
        public string CommercialName { get; set; }
        public string Description { get; set; }
        public string Presentation { get; set; }
        public string Concentration { get; set; }
        public string Indications { get; set; }
    }
}
