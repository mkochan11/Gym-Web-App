namespace Web.ViewModels.TrainingPlan.ClientDetails
{
    public class ClientTrainingPlanDetailsViewModel
    {
        public ClientTrainingPlanDetailsItemViewModel TrainingPlanItem { get; set; }
        public List<ClientTrainingPlanDetailsExerciseItemViewModel> ExerciseItems { get; set; } = new List<ClientTrainingPlanDetailsExerciseItemViewModel>();
    }
}
