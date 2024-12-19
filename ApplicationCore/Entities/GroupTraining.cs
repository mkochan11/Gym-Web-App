using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class GroupTraining : Training
    {
        public int GroupTrainerId { get; set; }

        [ForeignKey(nameof(GroupTrainerId))]
        public GroupTrainer GroupTrainer { get; set; }

        public int MaxParticipantNumber { get; set; }

        public List<Client> Participants { get; set; }

        public int TrainingTypeId { get; set; }

        [ForeignKey(nameof(TrainingTypeId))]
        public TrainingType TrainingType { get; set; }

    }
}
