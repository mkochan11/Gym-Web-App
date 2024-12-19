using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class GymMembership : BaseEntity
    {
        public int MembershipPlanId { get; set; }

        [ForeignKey(nameof(MembershipPlanId))]
        public MembershipPlan MembershipPlan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
