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
        public List<Training> Trainings { get; set; } = new List<Training>();
        public List<TrainingPlan> TrainingPlans { get; set;} = new List<TrainingPlan>();
    }
}
