namespace RXDigital.Api.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string SocialNumber { get; set; }
        public char Gender { get; set; }
        public string Nationality { get; set; }
        //public string Address { get; set; }
        public string? Cellphone { get; set; }
        public string? HomePhone { get; set; }
        //public string Email { get; set; }

        public int SocialWorkId { get; set; }

        public SocialWork SocialWork { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
