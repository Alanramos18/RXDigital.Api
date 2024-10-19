using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RXDigital.Api.DTOs;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IAccountService accountService, ILogger<LoginController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        ///     Create new accounts.
        /// </summary>
        /// <param name="createAccountDto">Create account dto</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created account dto</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bad Request.</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDto createAccountDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _accountService.RegisterAsync(createAccountDto, cancellationToken);

                if (response == null)
                {

                    return new BadRequestObjectResult("That email is already registered.");
                }

                return new CreatedAtRouteResult("RegisterAsync", response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _accountService.LoginAsync(dto, cancellationToken);

                return new OkObjectResult(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}