using Web.ViewModels.Calendar.Trainings;

namespace Web.Interfaces
{
    public interface ITrainingsCalendarViewModelService
    {
        Task<TrainingsCalendarIndexViewModel> GetTrainingsCalendarIndexViewModel(int month, int year, string userId);
    }
}
