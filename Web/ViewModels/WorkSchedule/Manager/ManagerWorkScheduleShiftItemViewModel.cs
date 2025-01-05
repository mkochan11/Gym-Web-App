using ApplicationCore.Entities;

namespace Web.ViewModels.WorkSchedule.Manager
{
    public class ManagerWorkScheduleShiftItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string ReceptionistName { get; set; }
        public int ReceptionistId { get; set; }
        public List<ManagerWorkScheduleReceptionistItemViewModelService> Receptionists { get; set; } = new List<ManagerWorkScheduleReceptionistItemViewModelService>();
    }
}
