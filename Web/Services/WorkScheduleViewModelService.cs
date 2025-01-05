﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System.Drawing.Text;
using Web.Interfaces;
using Web.ViewModels.Calendar.Trainer.Trainings.Group;
using Web.ViewModels.WorkSchedule.Manager;

namespace Web.Services
{
    public class WorkScheduleViewModelService : IWorkScheduleViewModelService
    {
        private readonly IRepository<Shift> _shiftRepository;
        private readonly IRepository<Receptionist> _receptionistRepository;

        public WorkScheduleViewModelService(
            IRepository<Shift> shiftRepository,
            IRepository<Receptionist> receptionistRepository)
        {
            _shiftRepository = shiftRepository;
            _receptionistRepository = receptionistRepository;
        }

        public async Task<ManagerWorkScheduleIndexViewModel> GetManagerWorkScheduleIndexViewModel(int currentMonth, int currentYear)
        {
            var shifts = await _shiftRepository.ListAsync(new FindShiftsByMonth(currentMonth, currentYear));

            var _shiftItems = new List<ManagerWorkScheduleShiftItemViewModel>();

            var receptionists = await _receptionistRepository.ListAsync();

            var _receptionistItems = new List<ManagerWorkScheduleReceptionistItemViewModelService>();

            foreach (var receptionist in receptionists)
            {
                _receptionistItems.Add(new ManagerWorkScheduleReceptionistItemViewModelService
                {
                    Id = receptionist.Id,
                    Name = receptionist.Name + " " + receptionist.Surname
                });
            }

            foreach (var shift in shifts)
            {
                shift.Receptionist = await _receptionistRepository.GetByIdAsync(shift.ReceptionistId);

                _shiftItems.Add(new ManagerWorkScheduleShiftItemViewModel
                {
                    Date = shift.StartTime,
                    Duration = shift.EndTime - shift.StartTime,
                    Id = shift.Id,
                    ReceptionistId = shift.ReceptionistId,
                    ReceptionistName = shift.Receptionist is null ? "Nie znaleziono recepcjonisty" : (shift.Receptionist.Name + " " + shift.Receptionist.Surname),
                    Receptionists = _receptionistItems
                });
            }

            var daysInMonth = GetManagerDaysInMonth(currentYear, currentMonth);

            foreach(var day in daysInMonth)
            {
                if (day != null)
                {
                    day.Shifts = _shiftItems.Where(x => x.Date.Day == day.Day).ToList();
                    day.Shifts = day.Shifts.OrderBy(x => x.Date.TimeOfDay).ToList();
                }
            }

            var viewModel = new ManagerWorkScheduleIndexViewModel
            {
                DaysInMonth = daysInMonth,
                Receptionists = _receptionistItems
            };

            return viewModel;

        }


        private List<ManagerWorkScheduleDayItemViewModel> GetManagerDaysInMonth(int year, int month)
        {
            List<ManagerWorkScheduleDayItemViewModel> days = new List<ManagerWorkScheduleDayItemViewModel>();
            var firstDayOfMonth = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);

            // znalezienie dnia tygodnia pierwszego dnia miesiąca
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            // dodanie pustych dni na początku miesiąca
            for (int i = 0; i < startDay; i++)
            {
                days.Add(null);
            }

            // dodanie dni w aktualnym miesiącu
            for (int i = 1; i <= daysInMonth; i++)
            {
                days.Add(new ManagerWorkScheduleDayItemViewModel
                {
                    Day = i,
                    Date = new DateTime(year, month, i)
                });
            }

            // Fill in the remaining empty days at the end (next month's days)
            int remainingDays = 42 - (startDay + daysInMonth); // 42 = 6 rows of 7 days
            for (int i = 0; i < remainingDays; i++)
            {
                days.Add(null); // Empty day
            }

            return days;
        }
    }
}
