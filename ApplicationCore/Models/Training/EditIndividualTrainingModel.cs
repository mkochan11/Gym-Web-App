namespace ApplicationCore.Models.Training
{
    public class EditIndividualTrainingModel
    {
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public required DateTime Hour { get; set; }
        public required TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
