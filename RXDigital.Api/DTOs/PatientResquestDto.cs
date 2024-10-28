namespace RXDigital.Api.DTOs
{
    public class PatientResquestDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime InscriptionDate { get; set; }
        public char Gender { get; set; }
        public string Nationality { get; set; }
        public string AddressStreet { get; set; }
        public short AddressNumber { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Cellphone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public int SocialWorkId { get; set; }
        public string SocialNumber { get; set; }
        public string AvalabilityStatus { get; set; }
    }
}
