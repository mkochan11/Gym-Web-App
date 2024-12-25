using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Client : User
    {
        public List<GymMembership> GymMemberships { get; set; } = new List<GymMembership>();
        public List<IndividualTraining> IndividualTrainings { get; set; } = new List<IndividualTraining>();
        public List<GroupTrainingParticipation> GroupTrainingsParticipations { get; set; } = new List<GroupTrainingParticipation>();
        public List<TrainingPlan> TrainingPlans { get; set;} = new List<TrainingPlan>();
    }
}
