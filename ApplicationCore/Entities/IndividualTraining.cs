using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class IndividualTraining : Training
    {
        public int PersonalTrainerId { get; set; }

        [ForeignKey(nameof(PersonalTrainerId))]
        public PersonalTrainer PersonalTrainer { get; set; }
        public int? ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }
    }
}
