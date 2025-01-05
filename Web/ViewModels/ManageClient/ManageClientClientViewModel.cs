namespace Web.ViewModels.ManageClient
{
    public class ManageClientClientViewModel
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ManageClientMembershipViewModel Membership { get; set; }
    }
}
