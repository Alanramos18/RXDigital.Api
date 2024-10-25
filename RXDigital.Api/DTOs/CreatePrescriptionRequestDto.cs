using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using RXDigital.Api.Controllers;
using RXDigital.Api.Entities;
using RXDigital.Api.Services;

namespace RXDigital.Api.DTOs
{
    public class CreatePrescriptionRequestDto
    {
        [Required]
        public int DoctorRegistration { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public string Diagnostic { get; set; }

        [Required]
        public string Indications { get; set; }

        [Required]
        public Channels Channels { get; set; }
    }
}
