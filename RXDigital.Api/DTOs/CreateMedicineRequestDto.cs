using System.ComponentModel.DataAnnotations;

namespace RXDigital.Api.DTOs
{
    public class CreateMedicineRequestDto
    {
        [Required]
        public string NombreComercial { get; set; }

        [Required]
        public string Presentacion { get; set; }

        [Required]
        public string Concentracion { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
