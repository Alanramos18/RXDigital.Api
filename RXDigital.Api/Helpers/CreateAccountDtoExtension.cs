using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;

namespace RXDigital.Api.Helpers
{
    public static class CreateAccountDtoExtension
    {
        public static AccountEntity Convert(this RegisterRequestDto dto)
        {
            if (dto == null)
                return null;

            var account = new AccountEntity
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Dni = dto.Dni,
                RoleId = (int)dto.RoleId,
                UserName = dto.FirstName + dto.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                // 0 - Pendiente, 1 - Aceptado, 2 - Rechazado
                Estado = 0
            };

            return account;
        }
    }
}
