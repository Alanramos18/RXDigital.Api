using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class SocialWorkRepository : BaseRepository<SocialWork>, ISocialWorkRepository
    {
        public SocialWorkRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }
    }
}
