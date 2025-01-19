using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public int GymMembershipId { get; set; }

        [ForeignKey(nameof(GymMembershipId))]
        public GymMembership GymMembership { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
