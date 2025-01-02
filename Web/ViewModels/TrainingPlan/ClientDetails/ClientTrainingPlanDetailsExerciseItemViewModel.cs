namespace Web.ViewModels.TrainingPlan.ClientDetails
{
    public class ClientTrainingPlanDetailsExerciseItemViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RepetitionsNumber { get; set; }
        public int SeriesNumber { get; set; }
        public TimeSpan RestTime { get; set; }
    }
}
