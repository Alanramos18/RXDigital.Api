using Microsoft.EntityFrameworkCore;
using RXDigital.Api.Context;
using RXDigital.Api.Entities;
using RXDigital.Api.Repositories.Interfaces;

namespace RXDigital.Api.Repositories
{
    public class PharmaceuticalRepository : BaseRepository<Pharmaceutical>, IPharmaceuticalRepository
    {
        public PharmaceuticalRepository(IRxDigitalContext rxDigitalContext) : base(rxDigitalContext) { }

        public async Task<GetPrescriptionsPharmaceuticalProc> GetPrescriptionsInfoAsync(string code, CancellationToken cancellationToken)
        {
            RxInfo rxInfo = new RxInfo();
            List<MedicineInfo> medicineInfoList = new List<MedicineInfo>();

            var res = new GetPrescriptionsPharmaceuticalProc();

            var cmd = _context.Database.GetDbConnection().CreateCommand();
            await _context.Database.OpenConnectionAsync(cancellationToken);

            cmd.CommandText = "BuscarReceta";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            var parameter = cmd.CreateParameter();
            parameter.ParameterName = "@CODIGO_RECETA";
            parameter.Value = code;
            cmd.Parameters.Add(parameter);


            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                if (!await reader.ReadAsync())
                {
                    return res;
                }
                
                rxInfo.NombrePaciente = reader.GetString(0);
                rxInfo.Dni = reader.GetInt32(1);
                rxInfo.ObraSocial = reader.GetString(2);
                rxInfo.PlanSocial = reader.GetString(3);
                rxInfo.NumeroAfiliado = reader.GetString(4);
                rxInfo.NombreMedico = reader.GetString(5);
                rxInfo.Matricula = reader.GetInt32(6);
                rxInfo.Especialidad = reader.GetString(7);
                rxInfo.Diagnostico = reader.GetString(8);
                rxInfo.Indicaciones = reader.GetString(9);
                rxInfo.FechaEmision = reader.GetDateTime(10);
                rxInfo.Expiracion = reader.GetDateTime(11);
                rxInfo.Estado = reader.GetString(12);
                rxInfo.MotivoRechazo = string.IsNullOrEmpty(reader.GetString(13)) ? "" : reader.GetString(13);

                await reader.NextResultAsync();

                while (await reader.ReadAsync())
                {
                    medicineInfoList.Add(new MedicineInfo
                    {
                        NombreComercial = reader.GetString(0),
                        Presentacion = reader.GetString(1),
                        Concentracion = reader.GetString(2),
                        Indicaciones = reader.GetString(3),
                        MedicineId = reader.GetInt32(4)
                    });
                }
            }

            res.RxInfo = rxInfo;
            res.MedicineList = medicineInfoList;

            return res;
        }
    }

    public class GetPrescriptionsPharmaceuticalProc
    {
        public RxInfo RxInfo { get; set; }
        public List<MedicineInfo> MedicineList { get; set; }
    }

    public class RxInfo
    {
        public string NombrePaciente { get; set; }
        public int Dni { get; set; }
        public string ObraSocial { get; set; }
        public string PlanSocial { get; set; }
        public string NumeroAfiliado { get; set; }
        public string NombreMedico { get; set; }
        public int Matricula { get; set; }
        public string Especialidad { get; set; }
        public string Diagnostico { get; set; }
        public string Indicaciones { get; set; }
        public string MotivoRechazo { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime Expiracion { get; set; }
        public string Estado { get; set; }

    }

    public class MedicineInfo
    {
        public string NombreComercial { get; set; }
        public string Presentacion { get; set; }
        public string Concentracion { get; set; }
        public string Indicaciones { get; set; }
        public int MedicineId { get; set; }
    }
}
