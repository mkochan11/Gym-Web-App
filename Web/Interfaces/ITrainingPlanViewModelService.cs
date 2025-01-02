using Ardalis.Result;
using Web.ViewModels.TrainingPlan.Add;
using Web.ViewModels.TrainingPlan.ClientDetails;
using Web.ViewModels.TrainingPlan.Edit;
using Web.ViewModels.TrainingPlan.Index;

namespace Web.Interfaces
{
    public interface ITrainingPlanViewModelService
    {
        Task<AddTrainingPlanIndexViewModel> GetAddTrainingPlanIndexViewModel(string userId);
        Task<TrainingPlansIndexViewModel> GetTrainingPlansIndexViewModel(string userId);
        Task<EditTrainingPlanIndexViewModel> GetEditTrainingPlanIndexViewModel(int planId, string userId);
        Task<ClientTrainingPlansIndexViewModel> GetClientTrainingPlanIndexViewModel(string userId);
        Task<ClientTrainingPlanDetailsViewModel> GetClientTrainingPlanDetailsViewModel(int planId, string userId);
        Task<Result> CheckIfClientCanSeeTrainingPlan(int planId, string userId);
    }
}
