﻿namespace Web.ViewModels.Calendar.Trainer.Trainings.Personal
{
    public class PersonalTrainingsCalendarIndexDayItemViewModel
    {
        public int? Day { get; set; }
        public DateTime? Date { get; set; }
        public List<TrainingsCalendarIndexIndividualTrainingItemViewModel> Trainings { get; set; } = new List<TrainingsCalendarIndexIndividualTrainingItemViewModel>();
    }
}
