using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class AccountRepository : UserManager<AccountEntity>, IAccountRepository
    {
        private readonly UserStore<AccountEntity, IdentityRole, RxDigitalContext, string, IdentityUserClaim<string>,
        IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>
        _store;

        public AccountRepository(IUserStore<AccountEntity> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<AccountEntity> passwordHasher,
        IEnumerable<IUserValidator<AccountEntity>> userValidators,
        IEnumerable<IPasswordValidator<AccountEntity>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<AccountEntity>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _store = (UserStore<AccountEntity, IdentityRole, RxDigitalContext, string, IdentityUserClaim<string>,
            IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>)store;
        }

        public async Task<bool> CheckRegisteredEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var emailExist = await _store.Context.Set<AccountEntity>().AnyAsync(x => x.Email.Equals(email));

            return emailExist;
        }

        public async Task<AccountEntity> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

                var user = await _store.Context.Set<AccountEntity>().FirstOrDefaultAsync(x => x.Email.Equals(email));

                return user;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Update(AccountEntity user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                _store.Context.Set<AccountEntity>().Update(user);
                await _store.Context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task Delete(AccountEntity user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                _store.Context.Set<AccountEntity>().Remove(user);
                await _store.Context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<AccountEntity> GetAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                var user = await _store.Context.Set<AccountEntity>().Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync(cancellationToken);

                return user;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<List<AccountEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ThrowIfDisposed();

                var user = await _store.Context.Set<AccountEntity>()
                    .Include(x => x.Doctor)
                    .Include(x => x.Pharmaceutical)
                    .Where(x => x.Estado == 0).ToListAsync(cancellationToken);

                return user;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
