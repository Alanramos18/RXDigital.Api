using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class PharmaceuticalRepository : BaseRepository<Pharmaceutical>, IPharmaceuticalRepository
    {
        public PharmaceuticalRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }
    }
}
