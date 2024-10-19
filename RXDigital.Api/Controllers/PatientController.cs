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
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<LoginController> _logger;

        public PatientController(IPatientService patientService, ILogger<LoginController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        ///     Get the basic information of the patient.
        /// </summary>
        /// <param name="patientId">Id of the patient</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Patient info</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bad Request.</response>
        [HttpGet]
        [Route("basicInfo/{patientId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBasicInformationAsync(int patientId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _patientService.GetBasicInformationAsync(patientId, cancellationToken);

                if (response == null)
                {

                    return new BadRequestObjectResult("That patient doesn't exist.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        ///     Get the basic information of the patient.
        /// </summary>
        /// <param name="patientId">Id of the patient</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Patient info</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bad Request.</response>
        [HttpGet]
        [Route("prescriptions/{patientId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<GetPrescriptionsProc>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrescriptionsAsync(int patientId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _patientService.GetPrescriptionsAsync(patientId, cancellationToken);

                if (response == null)
                {

                    return new BadRequestObjectResult("That patient doesn't exist.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}