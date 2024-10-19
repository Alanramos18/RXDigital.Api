using Microsoft.AspNetCore.Identity;
using RXDigital.Api.Entities;

namespace RXDigital.Api.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        ///     Check whether an email in that source exists.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task<bool> CheckRegisteredEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        ///     Adds the specific entity.
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <param name="password">Cancellation Transaction Token</param>
        Task<IdentityResult> CreateAsync(AccountEntity entity, string password);

        Task<AccountEntity> FindByEmailAsync(string email, CancellationToken cancellationToken);

        Task<bool> CheckPasswordAsync(AccountEntity user, string password);
    }
}
