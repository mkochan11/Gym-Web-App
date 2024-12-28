namespace ApplicationCore.Entities.Abstract
{
    public abstract class User : BaseEntity
    {
        public required string AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}
