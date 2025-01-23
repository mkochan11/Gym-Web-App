namespace ApplicationCore.Models.Training
{
    public class EditGroupTrainingModel
    {
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public required DateTime Hour { get; set; }
        public required TimeSpan Duration { get; set; }
        public string? Description { get; set; } = string.Empty;
        public int TrainingTypeId { get; set; }
        public int MaxParticipantNumber { get; set; }
    }
}
