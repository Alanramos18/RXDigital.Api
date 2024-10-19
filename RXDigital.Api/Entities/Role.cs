namespace RXDigital.Api.Entities
{
    public class Role
    {
        /// <summary>
        ///     Role of the user.
        /// </summary>
        public int RoleId { get; set; }

        public string Description { get; set; }

        public IEnumerable<AccountEntity> Accounts { get; set; }
    }
}
