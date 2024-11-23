using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {

        public PatientRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }

        public async Task<List<GetPrescriptionsProc>> GetPrescriptionsAsync(int patientId)
        { 
            var res = await _context.Set<GetPrescriptionsProc>()
            .FromSqlRaw("EXEC ObtenerRecetas @Dni_paciente = {0}", patientId)
            .ToListAsync();

            return res;
        }
    }

    public class GetPrescriptionsProc
    {
        public string CodigoReceta { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Diagnostic { get; set; }
        public string? MedicineName { get; set; }
        public string Concentration { get; set; }
        public string? Comentario { get; set; }
        public string? Estado { get; set; }
    }
}
