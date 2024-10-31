using RXDigital.Api.DTOs;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<string> CreatePrescriptionAsync(CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken);
        Task DeletePrescriptionAsync(int prescriptionId, CancellationToken cancellationToken);
        Task<DoctorInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<List<MedicineInfoResponseDto>> SearchMedicinesAsync(string medicineName, CancellationToken cancellationToken);
    }
}
