using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }

        public async Task<List<GetFilteredRxProc>> GetPrescriptionsAsync(string from, string to, CancellationToken cancellationToken)
        {
            var res = await _context.Set<GetFilteredRxProc>()
            .FromSqlRaw("EXEC ConsultarRecetas @Desde = {0}, @Hasta ={1}", from, to)
            .ToListAsync(cancellationToken);

            return res;
        }

        public async Task<List<GetTopRxProc>> GetTopRxAsync(string top, CancellationToken cancellationToken)
        {
            var res = await _context.Set<GetTopRxProc>()
            .FromSqlRaw("EXEC TopMedicamentos @TOPRANK = {0}", top)
            .ToListAsync(cancellationToken);

            return res;
        }

        public async Task<List<GetTopMedicsProc>> GetTopMedicsAsync(string top, CancellationToken cancellationToken)
        {
            var res = await _context.Set<GetTopMedicsProc>()
            .FromSqlRaw("EXEC TopMedicos @TOPRANK = {0}", top)
            .ToListAsync(cancellationToken);

            return res;
        }
    }

    public class GetFilteredRxProc
    {
        public string CodigoReceta { get; set; }
        public DateTime FechaEmision { get; set; }
        public string? PatientName { get; set; }
        public string? MedicineName { get; set; }
        public string? Estado { get; set; }
    }

    public class GetTopRxProc
    {
        public string? NombreComercial { get; set; }
        public string? Presentacion { get; set; }
        public string? Concentracion { get; set; }
        public int TotalRecetado { get; set; }
    }

    public class GetTopMedicsProc
    {
        public int Matricula { get; set; }
        public string? NombreMedico { get; set; }
        public int CantidadReceta { get; set; }
    }
}
