namespace RXDigital.Api.Entities
{
    public class Pharmaceutical
    {
        public int RegistrationId { get; set; }
        public string AccountId { get; set; }

        public AccountEntity Account { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
