using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RXDigital.Api.DTOs;
using RXDigital.Api.Repositories;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<LoginController> _logger;

        public DoctorController(IDoctorService doctorService, ILogger<LoginController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDoctorInfoAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var doc = await _doctorService.GetByUserIdAsync(userId, cancellationToken);

                if (doc.RegistrationId == 0)
                {
                    return NotFound();
                }

                return new OkObjectResult(doc);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("searchMedicines")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<MedicineInfoResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchMedicinesAsync([FromQuery]string medicineName, CancellationToken cancellationToken = default)
        {
            try
            {
                var medicineList = await _doctorService.SearchMedicinesAsync(medicineName, cancellationToken);

                if (medicineList.Count == 0)
                {
                    return NotFound();
                }

                return new OkObjectResult(medicineList);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("create-prescription")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePrescriptionAsync(CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                await _doctorService.CreatePrescriptionAsync(requestDto, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-prescription")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePrescriptionAsync(int rxId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _doctorService.DeletePrescriptionAsync(rxId, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }

    public enum Channels
    {
        None = 0,
        Whatsapp = 1,
        Email = 2
    }
}