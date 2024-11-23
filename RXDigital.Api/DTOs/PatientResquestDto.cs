namespace RXDigital.Api.DTOs
{
    public class PatientResquestDto
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Celular { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; }
        public int ObraSocialId { get; set; }
        public string? NumeroAfiliado { get; set; }
    }
}
