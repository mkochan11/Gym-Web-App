using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class PersonalTrainer : Trainer<IndividualTraining>
    {
        public List<TrainingPlan> Plans { get; set; } = new List<TrainingPlan>();
    }
}
