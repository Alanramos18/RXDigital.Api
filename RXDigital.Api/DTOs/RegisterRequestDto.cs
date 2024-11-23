using System.ComponentModel.DataAnnotations;
using RXDigital.Api.Services;

namespace RXDigital.Api.DTOs
{
    public class RegisterRequestDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public int Dni { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public RoleEnum RoleId { get; set; }

        public int? Registration { get; set; }
        public int? EspecialidadId { get; set; }
    }
}
