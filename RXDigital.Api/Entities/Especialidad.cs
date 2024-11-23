namespace RXDigital.Api.Entities
{
    public class Especialidad
    {
        public int EspecialidadId { get; set; }
        public string Descripcion { get; set; }

        public Doctor Doctor { get; set; }
    }
}
