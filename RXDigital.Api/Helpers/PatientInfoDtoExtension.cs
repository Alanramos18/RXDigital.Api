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
                Id = entity.PatientId,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };

            return patientInfo;
        }
    }
}
