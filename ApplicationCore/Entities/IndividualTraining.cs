using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class IndividualTraining : Training
    {
        public int PersonalTrainerId { get; set; }

        [ForeignKey(nameof(PersonalTrainerId))]
        public PersonalTrainer PersonalTrainer { get; set; }

        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
    }
}
