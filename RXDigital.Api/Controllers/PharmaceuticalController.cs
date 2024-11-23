using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RXDigital.Api.DTOs;
using RXDigital.Api.Repositories;
using RXDigital.Api.Services;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Controllers
{
    [ApiController]
    //[Authorize(Policy = "PharmaceuticPolicy")]
    [AllowAnonymous]
    [Route("[controller]")]
    public class PharmaceuticalController : ControllerBase
    {
        private readonly IPharmaceuticalService _pharmaceuticalService;

        public PharmaceuticalController(IPharmaceuticalService pharmaceuticalService, ILogger<LoginController> logger)
        {
            _pharmaceuticalService = pharmaceuticalService;
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDoctorInfoAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var doc = await _pharmaceuticalService.GetByUserIdAsync(userId, cancellationToken);

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
        [Route("get-presciption-info/{rxCode}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetPrescriptionsPharmaceuticalProc))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrescriptionInformationAsync(string rxCode, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _pharmaceuticalService.GetPrescriptionInformationAsync(rxCode, cancellationToken);

                if (response == null || response.RxInfo == null)
                {

                    return new BadRequestObjectResult("That rx doesn't exist.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("rx/{rxCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptRxAsync(string rxCode, [FromQuery]int matricula, CancellationToken cancellationToken = default)
        {
            try
            {
                await _pharmaceuticalService.AcceptRxAsync(rxCode, matricula, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("rx")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectRxAsync(RejectRxResquestDto rejectRxResquestDto, CancellationToken cancellationToken = default)
        {
            try
            {
                await _pharmaceuticalService.RejectRxAsync(rejectRxResquestDto, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}