using Web.ViewModels.Calendar.Client.Trainings;
using Web.ViewModels.Calendar.Trainer.Trainings.Group;
using Web.ViewModels.Calendar.Trainer.Trainings.Personal;

namespace Web.Interfaces
{
    public interface ITrainingsCalendarViewModelService
    {
        Task<ClientTrainingsCalendarIndexViewModel> GetClientTrainingsCalendarIndexViewModel(int month, int year, string userId);
        Task<PersonalTrainingsCalendarIndexViewModel> GetPersonalTrainingsCalendarIndexViewModel(int month, int year, string userId);
        Task<GroupTrainingsCalendarIndexViewModel> GetGroupTrainingsCalendarIndexViewModel(int month, int year, string userId);
        Task<List<GroupTrainingsTypeItemViewModel>> GetGroupTrainingsTypeItemViewModels();
        
    }
}
