using Web.ViewModels.Calendar.Client.Trainings;

namespace Web.Interfaces
{
    public interface ITrainingsCalendarViewModelService
    {
        Task<ClientTrainingsCalendarIndexViewModel> GetTrainingsCalendarIndexViewModel(int month, int year, string userId);
        
    }
}
