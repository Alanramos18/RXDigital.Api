namespace RXDigital.Api.Entities
{
    public class Doctor
    {
        public int RegistrationId { get; set; }
        public string UserId { get; set; }
        public int EspecialidadId { get; set; }

        public AccountEntity User { get; set; }
        public Especialidad Especialidad { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
