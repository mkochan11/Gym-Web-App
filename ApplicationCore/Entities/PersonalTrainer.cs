using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class PersonalTrainer : Trainer<IndividualTraining>
    {
        public List<TrainingPlan> Plans { get; set; } = new List<TrainingPlan>();
    }
}
