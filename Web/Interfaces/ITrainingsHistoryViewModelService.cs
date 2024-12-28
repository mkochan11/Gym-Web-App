using Web.ViewModels.Calendar.Client.TrainingsHistory;

namespace Web.Interfaces
{
    public interface ITrainingsHistoryViewModelService
    {
        Task<ClientTrainingsHistoryIndexViewModel> GetClientTrainingsHistoryIndexViewModel(int month, int year, string userId);
    }
}
