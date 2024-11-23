using RXDigital.Api.Entities;

namespace RXDigital.Api.DTOs
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public int Matricula { get; set; }
    }
}
