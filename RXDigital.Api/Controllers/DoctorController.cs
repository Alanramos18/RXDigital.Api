using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Controllers
{
    [ApiController]
    //[Authorize(Policy = "MedicPolicy")]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
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

        [HttpPut]
        [Route("update-prescription/{rxCode}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePrescriptionAsync(string rxCode, CreatePrescriptionRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                await _doctorService.UpdatePrescriptionAsync(rxCode, requestDto, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-prescription/{prescriptionCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Prescription))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken = default)
        {
            try
            {
                var res = await _doctorService.GetPrescriptionAsync(prescriptionCode, cancellationToken);

                if (res == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-prescription/{prescriptionCode}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePrescriptionAsync(string prescriptionCode, CancellationToken cancellationToken = default)
        {
            try
            {
                await _doctorService.DeletePrescriptionAsync(prescriptionCode, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}