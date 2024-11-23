using Microsoft.EntityFrameworkCore;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Helpers;
using RXDigital.Api.Repositories;
using RXDigital.Api.Repositories.Interfaces;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IEmailService _emailService;

        public AdminService(IMedicineRepository medicineRepository, ISpecialityRepository specialityRepository,
            IAccountRepository accountRepository, IPrescriptionRepository prescriptionRepository,
            IEmailService emailService)
        {
            _medicineRepository = medicineRepository;
            _specialityRepository = specialityRepository;
            _accountRepository = accountRepository;
            _prescriptionRepository = prescriptionRepository;
            _emailService = emailService;
        }

        public async Task<AdminInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var admin = await _accountRepository.GetAsync(userId, cancellationToken);
            var adminResponse = new AdminInfoResponseDto();

            if (admin != null)
            {
                adminResponse.FirstName = admin.FirstName;
                adminResponse.LastName = admin.LastName;
                adminResponse.Dni = admin.Dni;
            }

            return adminResponse;
        }

        public async Task<List<Especialidad>> GetSpecialitiesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var list = await _specialityRepository.Get().ToListAsync(cancellationToken);

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<UserResponseDto>();
                var res = await _accountRepository.GetAllAsync(cancellationToken);

                foreach (var user in res)
                {
                    int mat = 0;
                    if (user.RoleId != 1)
                    {
                        mat = user.RoleId == 2 ? user.Doctor.RegistrationId : user.Pharmaceutical.RegistrationId;
                    }

                    list.Add(
                        new UserResponseDto
                        {
                            Id = user.Id,
                            Nombre = user.FirstName,
                            Apellido = user.LastName,
                            Dni = user.Dni,
                            RoleId = user.RoleId,
                            Email = user.Email,
                            Matricula = mat
                        }
                    );
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task ApproveUserAsync(string userId, bool isApproved, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _accountRepository.GetAsync(userId, cancellationToken);
                var fullName = $"{user.LastName}, {user.FirstName}";

                if (isApproved)
                {
                    // 0 - Pendiente, 1 - Aceptado, 2 - Rechazado
                    user.Estado = 1;
                    await _accountRepository.Update(user, cancellationToken);
                    await _emailService.SendApprovedEmailAsync(user.Email, fullName, cancellationToken);
                }
                else
                {
                    await _emailService.SendRejectEmailAsync(user.Email, fullName, cancellationToken);
                    await _accountRepository.Delete(user, cancellationToken);
                }

                return;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <inheritdoc />
        public async Task CreateMedicineAsync(CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken)
        {
            try
            {
                var med = new Medicine
                {
                    CommercialName = medicineDto.NombreComercial,
                    Presentation = medicineDto.Presentacion,
                    Concentration = medicineDto.Concentracion,
                    Description = medicineDto.Descripcion
                };

                await _medicineRepository.AddAsync(med, cancellationToken);
                await _medicineRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateMedicineAsync(int medId, CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken)
        {
            try
            {
                var med = await _medicineRepository.GetByIdAsync(medId, cancellationToken);

                med.CommercialName = medicineDto.NombreComercial;
                med.Presentation = medicineDto.Presentacion;
                med.Concentration = medicineDto.Concentracion;
                med.Description = medicineDto.Descripcion;

                _medicineRepository.Update(med);
                await _medicineRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteMedicineAsync(int medicineId, CancellationToken cancellationToken)
        {
            try
            {
                var med = await _medicineRepository.GetByIdAsync(medicineId, cancellationToken);

                _medicineRepository.Delete(med);

                await _medicineRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<GetFilteredRxProc>> GetFilterRxAsync(string from, string to, CancellationToken cancellationToken)
        {
            var res = await _prescriptionRepository.GetPrescriptionsAsync(from, to, cancellationToken);

            return res;
        }

        public async Task<List<GetTopRxProc>> GetTopRxAsync(string top, CancellationToken cancellationToken)
        {
            var res = await _prescriptionRepository.GetTopRxAsync(top, cancellationToken);

            return res;
        }

        public async Task<List<GetTopMedicsProc>> GetTopMedicAsync(string top, CancellationToken cancellationToken)
        {
            var res = await _prescriptionRepository.GetTopMedicsAsync(top, cancellationToken);

            return res;
        }
    }
}
