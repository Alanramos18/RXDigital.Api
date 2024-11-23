using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class PrescriptionMedicineRepository : BaseRepository<PrescriptionMedicine>, IPrescriptionMedicineRepository
    {
        public PrescriptionMedicineRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }
    }
}
