using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RXDigital.Api.DTOs;
using RXDigital.Api.Repositories;
using RXDigital.Api.Services;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Controllers
{
    [ApiController]
    //[Authorize(Policy = "AdminPolicy")]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientInfoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDoctorInfoAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var doc = await _adminService.GetByUserIdAsync(userId, cancellationToken);

                if (doc == null)
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
        
        [HttpPost]
        [Route("create-med")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMedicineAsync(CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken = default)
        {
            try
            {
                await _adminService.CreateMedicineAsync(medicineDto, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("modify-med/{medId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMedicineAsync(int medId, CreateMedicineRequestDto medicineDto, CancellationToken cancellationToken = default)
        {
            try
            {
                await _adminService.UpdateMedicineAsync(medId, medicineDto, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete-med")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMedicineAsync([FromQuery]int medicineId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _adminService.DeleteMedicineAsync(medicineId, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-specialities")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSpecialitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _adminService.GetSpecialitiesAsync(cancellationToken);

                if (response == null)
                {
                    return new BadRequestObjectResult("Error.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-users")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _adminService.GetUsersAsync(cancellationToken);

                if (response == null)
                {
                    return new BadRequestObjectResult("Error.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("review-users/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReviewAsync(string userId, [FromQuery] bool isApproved, CancellationToken cancellationToken = default)
        {
            try
            {
                await _adminService.ApproveUserAsync(userId, isApproved, cancellationToken);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-filter-rx")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFilterRxAsync([FromQuery] string from, [FromQuery] string to, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _adminService.GetFilterRxAsync(from, to, cancellationToken);

                if (response == null)
                {
                    return new BadRequestObjectResult("Error.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-top-rx")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTopRxAsync([FromQuery] string top, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _adminService.GetTopRxAsync(top, cancellationToken);

                if (response == null)
                {
                    return new BadRequestObjectResult("Error.");
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-top-medics")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTopMedicsAsync([FromQuery] string top, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _adminService.GetTopMedicAsync(top, cancellationToken);

                if (response == null)
                {
                    return new BadRequestObjectResult("Error.");
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