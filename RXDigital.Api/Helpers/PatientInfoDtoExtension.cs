using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;

namespace RXDigital.Api.Helpers
{
    public static class PatientInfoDtoExtension
    {
        public static PatientInfoResponseDto Convert(this Patient entity)
        {
            if (entity == null)
                return null;

            var patientInfo = new PatientInfoResponseDto
            {
                Dni = entity.Dni,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                FechaNacimiento = entity.FechaNacimiento,
                FechaInscripcion = entity.FechaInscripcion,
                NumeroAfiliado = entity.NumeroAfiliado,
                Genero = entity.Genero,
                Celular = entity.Celular,
                Telefono = entity.Telefono,
                ObraSocial = entity.SocialWork.Name,
                PlanSocial = entity.SocialWork.SocialPlan,
                Domicilio = entity.Domicilio,
                Email = entity.Email,
                Nacionalidad = entity.Nacionalidad,
                Habilitacion = entity.Habilitado,
                Localidad = entity.Localidad,
                Provincia = entity.Provincia
            };

            return patientInfo;
        }
    }
}
