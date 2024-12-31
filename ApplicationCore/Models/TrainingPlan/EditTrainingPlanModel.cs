namespace ApplicationCore.Models.TrainingPlan
{
    public class EditTrainingPlanModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
    }
}
