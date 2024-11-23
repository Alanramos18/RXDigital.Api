using System.Threading;
using System.Threading.Tasks;
using RXDigital.Api.Services;

namespace RXDigital.Api.Helpers
{
    public interface IEmailService
    {
        /// <summary>
        ///     Send email.
        /// </summary>
        /// <param name="to">Email that will received the mail</param>
        /// <param name="link">Verification link</param>
        /// <param name="name">User Name</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="from">Email Sender</param>
        /// <returns></returns>
        Task SendVerificationAsync(string to, string link, string name, CancellationToken cancellationToken, string from = null);

        /// <summary>
        ///     Send reset code email
        /// </summary>
        /// <param name="to">Destination email</param>
        /// <param name="code">Reset code</param>
        /// <param name="name">User Name</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="from">Origin email sender</param>
        Task SendRejectEmailAsync(string to, string name, CancellationToken cancellationToken, string from = null);
        Task SendApprovedEmailAsync(string to, string name, CancellationToken cancellationToken, string from = null);
        Task SendRxEmailAsync(string to, AllInfo allInfo, CancellationToken cancellationToken, string from = null);
        Task SendUpdatedRxEmailAsync(string to, AllInfo allInfo, CancellationToken cancellationToken, string from = null);
    }
}
