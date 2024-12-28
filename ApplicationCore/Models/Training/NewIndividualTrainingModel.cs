
namespace ApplicationCore.Models.Training
{
    public class NewIndividualTrainingModel
    {
        public required DateTime Date { get; set; }
        public required TimeSpan Duration { get; set; }
        public required DateTime Hour {  get; set; }
        public string Description { get; set; } = string.Empty;
        public required bool IsCyclic { get; set; }
        public required string? Repeatability { get; set; }
    }
}
