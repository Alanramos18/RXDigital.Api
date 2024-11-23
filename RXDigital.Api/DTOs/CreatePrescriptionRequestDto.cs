using System.ComponentModel.DataAnnotations;
using RXDigital.Api.Controllers;

namespace RXDigital.Api.DTOs
{
    public class CreatePrescriptionRequestDto
    {
        [Required]
        public int DoctorRegistration { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public List<MedicinesDto> Medicines { get; set; }

        [Required]
        public string Diagnostic { get; set; }

        public string? Indications { get; set; }
    }

    public class MedicinesDto
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        public string Indications { get; set; }
    }
}
