using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IAdminService
    {
        Task CreateMedicineAsync(CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken);
        Task UpdateMedicineAsync(int medId, CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken);
        Task DeleteMedicineAsync(int medicineId, CancellationToken cancellationToken);
        Task<List<Especialidad>> GetSpecialitiesAsync(CancellationToken cancellationToken);
        Task<List<UserResponseDto>> GetUsersAsync(CancellationToken cancellationToken);
        Task<List<GetFilteredRxProc>> GetFilterRxAsync(string from, string to, CancellationToken cancellationToken);
        Task<List<GetTopRxProc>> GetTopRxAsync(string top, CancellationToken cancellationToken);
        Task<List<GetTopMedicsProc>> GetTopMedicAsync(string top, CancellationToken cancellationToken);
        Task<AdminInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task ApproveUserAsync(string userId, bool isApproved, CancellationToken cancellationToken);
    }
}
