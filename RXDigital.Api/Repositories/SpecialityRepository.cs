using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class SpecialityRepository : BaseRepository<Especialidad>, ISpecialityRepository
    {
        public SpecialityRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }
    }
}
