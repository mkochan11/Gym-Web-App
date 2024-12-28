namespace ApplicationCore.Entities.Abstract
{
    public abstract class Training : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
