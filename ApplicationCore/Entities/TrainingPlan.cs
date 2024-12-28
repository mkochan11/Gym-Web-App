using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class TrainingPlan : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
        
        public int PersonalTrainerId { get; set; }
        [ForeignKey(nameof(PersonalTrainerId))]
        public PersonalTrainer PersonalTrainer { get; set; }
    }
}
