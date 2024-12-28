namespace Web.ViewModels.Membership
{
    public class MembershipIndexItemViewModel
    {
        public int Id { get; set; }
        public string PlanType { get; set; }
        public string PlanDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}