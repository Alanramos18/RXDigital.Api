using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<string> CreatePrescriptionAsync(CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken);
        Task UpdatePrescriptionAsync(string rxCode, CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken);
        Task DeletePrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken);
        Task<DoctorInfoResponseDto> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<List<MedicineInfoResponseDto>> SearchMedicinesAsync(string medicineName, CancellationToken cancellationToken);
        Task<Prescription> GetPrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken);
    }
}
