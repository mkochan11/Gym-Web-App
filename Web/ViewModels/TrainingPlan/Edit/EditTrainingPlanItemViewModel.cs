namespace Web.ViewModels.TrainingPlan.Edit
{
    public class EditTrainingPlanItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public List<EditTrainingPlanExerciseItemViewModel> ExerciseItems { get; set; } = new List<EditTrainingPlanExerciseItemViewModel>();
    }
}
