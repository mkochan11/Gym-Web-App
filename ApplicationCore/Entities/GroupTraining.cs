﻿using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class GroupTraining : Training
    {
        public int GroupTrainerId { get; set; }

        [ForeignKey(nameof(GroupTrainerId))]
        public GroupTrainer GroupTrainer { get; set; }

        public int MaxParticipantNumber { get; set; }

        public List<GroupTrainingParticipation> Participations { get; set; }

        public int TrainingTypeId { get; set; }

        [ForeignKey(nameof(TrainingTypeId))]
        public TrainingType TrainingType { get; set; }

    }
}
