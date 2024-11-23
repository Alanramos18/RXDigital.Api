using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Context;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
                where T : class
    {
        protected readonly IRxDigitalContext _context;
        internal DbSet<T> _dbSet;

        protected BaseRepository(IRxDigitalContext instituteContext)
        {
            _context = instituteContext;
            this._dbSet = (_context as DbContext)?.Set<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        /// <inheritdoc />
        public virtual async Task<T> GetByIdAsync(int entityId, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(new object[] { entityId }, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await (_context as DbContext).SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
