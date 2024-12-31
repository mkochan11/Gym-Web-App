using System.Net;

namespace Web.ViewModels.TrainingPlan.Edit
{
    public class EditTrainingPlanIndexViewModel
    {
        public EditTrainingPlanItemViewModel TrainingPlanItem { get; set; }
        public List<EditTrainingPlanClientItemViewModel> ClientItems { get; set; }
    }
}
