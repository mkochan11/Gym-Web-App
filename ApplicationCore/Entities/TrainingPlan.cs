using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
