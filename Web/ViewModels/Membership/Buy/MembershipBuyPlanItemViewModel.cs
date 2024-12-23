namespace Web.ViewModels.Membership.Buy
{
    public class MembershipBuyPlanItemViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DurationTime { get; set; }
    }
}
