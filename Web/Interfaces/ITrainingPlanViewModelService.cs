using Web.ViewModels.TrainingPlan.Add;

namespace Web.Interfaces
{
    public interface ITrainingPlanViewModelService
    {
        Task<AddTrainingPlanIndexViewModel> GetAddTrainingPlanIndexViewModel(string userId);
    }
}
