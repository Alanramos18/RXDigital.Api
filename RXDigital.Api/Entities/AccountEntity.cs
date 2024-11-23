using Microsoft.AspNetCore.Identity;

namespace RXDigital.Api.Entities
{
    public class AccountEntity : IdentityUser
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
        public int RoleId { get; set; }
        public int Estado { get; set; }

        public Role Role { get; set; }
        public Doctor? Doctor { get; set; }
        public Pharmaceutical? Pharmaceutical { get; set; }
    }
}
