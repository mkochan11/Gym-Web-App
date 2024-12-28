using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class GroupTrainingParticipation : BaseEntity
    {
        public int GroupTrainingId { get; set; }
        [ForeignKey(nameof(GroupTrainingId))]
        public GroupTraining GroupTraining { get; set; }
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
    }
}
