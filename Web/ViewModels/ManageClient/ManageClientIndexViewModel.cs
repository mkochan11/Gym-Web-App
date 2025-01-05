namespace Web.ViewModels.ManageClient
{
    public class ManageClientIndexViewModel
    {
        public ManageClientClientViewModel Client { get; set; }
        public List<ManageClientMembershipPlanItemViewModel> MembershipPlans { get; set; }
    }
}
