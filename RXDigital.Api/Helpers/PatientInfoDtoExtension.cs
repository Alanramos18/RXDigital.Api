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
                PatientId = entity.PatientId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDay = entity.BirthDay,
                InscriptionDate = entity.InscriptionDate,
                SocialNumber = entity.SocialNumber,
                Gender = entity.Gender,
                Cellphone = entity.Cellphone,
                HomePhone = entity.HomePhone,
                SocialWorkName = entity.SocialWork.Name,
                SocialPlan = entity.SocialWork.SocialPlan,
                Address = $"{entity.AddressStreet} {entity.AddressNumber}",
                Email = entity.Email,
                Nationality = entity.Location.Country,
                IsAvailable = entity.IsAvailable
            };

            return patientInfo;
        }
    }
}
