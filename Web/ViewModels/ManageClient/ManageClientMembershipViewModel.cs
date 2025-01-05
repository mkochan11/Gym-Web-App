namespace Web.ViewModels.ManageClient
{
    public class ManageClientMembershipViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ManageClientMembershipPlanItemViewModel MembershipPlan { get; set; }
    }
}
