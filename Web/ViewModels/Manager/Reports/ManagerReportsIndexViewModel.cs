using Microsoft.Build.ObjectModelRemoting;

namespace Web.ViewModels.Manager.Reports
{
    public class ManagerReportsIndexViewModel
    {
        public List<ManagerReportsIndexItemViewModel> Reports { get; set; } = new List<ManagerReportsIndexItemViewModel>();
        public List<ManagerReportsEmployeeItemViewModel> Employees { get; set; } = new List<ManagerReportsEmployeeItemViewModel>();
    }
}
