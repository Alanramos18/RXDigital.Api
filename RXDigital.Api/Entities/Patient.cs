namespace RXDigital.Api.Entities
{
    public class Patient
    {
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Email { get; set; }
        public string NumeroAfiliado { get; set; }
        public char Genero { get; set; }
        public string? Celular { get; set; }
        public string? Telefono { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Nacionalidad { get; set; }
        public bool Habilitado { get; set; }

        public int SocialWorkId { get; set; }

        public SocialWork SocialWork { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
