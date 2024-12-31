using ApplicationCore.Models.Exercise;

namespace ApplicationCore.Models.TrainingPlan
{
    public class NewTrainingPlanModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public List<NewExerciseModel> Exercises { get; set; }
    }
}
