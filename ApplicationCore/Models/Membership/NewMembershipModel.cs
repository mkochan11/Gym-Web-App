namespace ApplicationCore.Models.Membership
{
    public class NewMembershipModel
    {
        public int MembershipPlanId {get; set;}
        public string UserId {get; set;}
        public string PaymentMethod {get; set;}
    }
}