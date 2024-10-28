namespace RXDigital.Api.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime InscriptionDate { get; set; }
        public string Email { get; set; }
        public string SocialNumber { get; set; }
        public char Gender { get; set; }
        public string? Cellphone { get; set; }
        public string? HomePhone { get; set; }
        public bool IsAvailable { get; set; }
        public string AddressStreet { get; set; }
        public short AddressNumber { get; set; }
        
        public int SocialWorkId { get; set; }
        public int LocationId { get; set; }

        public SocialWork SocialWork { get; set; }
        public Location Location { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
