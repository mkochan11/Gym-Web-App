using ApplicationCore.Enums;

namespace Web.ViewModels.Manager.Reports
{
    public class ManagerReportsEmployeeItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
    }
}
