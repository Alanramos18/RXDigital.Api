using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Context;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }
    }
}
