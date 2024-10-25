using RXDigital.Api.DTOs;

namespace RXDigital.Api.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        ///     Create account service.
        /// </summary>
        /// <param name="accountDto">Create account Dto</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created account dto</returns>
        Task<string> RegisterAsync(RegisterRequestDto accountDto, CancellationToken cancellationToken);

        /// <summary>
        ///     Login user to get token.
        /// </summary>
        /// <param name="dto">Login dto</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>JWT string</returns>
        Task<string> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken);
    }
}
