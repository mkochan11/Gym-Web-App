
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Shift;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class ShiftService : IShiftService
    {
        private IRepository<Receptionist> _receptionistRepository;
        private IRepository<Shift> _shiftRepository;

        public ShiftService(IRepository<Receptionist> receptionistRepository, IRepository<Shift> shiftRepository)
        {
            _receptionistRepository = receptionistRepository;
            _shiftRepository = shiftRepository;
        }
        public async Task<Result> CreateShift(NewShiftModel model)
        {
            var receptionist = await _receptionistRepository.GetByIdAsync(model.ReceptionistId);

            if (receptionist == null)
            {
                return Result.NotFound();
            }

            var shift = new Shift
            {
                StartTime = model.Date.AddHours(model.StartHour.Hour),
                EndTime = model.Date.AddHours(model.EndHour.Hour),
                ReceptionistId = model.ReceptionistId
            };

            await _shiftRepository.AddAsync(shift);

            return Result.Success();
        }

        public async Task<Result> DeleteShift(int shiftId)
        {
            var shift = await _shiftRepository.GetByIdAsync(shiftId);

            if (shift == null)
            {
                return Result.NotFound();
            }

            await _shiftRepository.DeleteAsync(shift);
            return Result.Success();
        }

        public async Task<Result> UpdateShift(EditShiftModel model)
        {
            var shift = await _shiftRepository.GetByIdAsync(model.ShiftId);

            if (shift == null)
            {
                return Result.NotFound();
            }

            var receptionist = await _receptionistRepository.GetByIdAsync(model.ReceptionistId);

            if (receptionist == null)
            {
                return Result.NotFound();
            }

            shift.StartTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, model.StartHour.Hour, 0, 0);
            shift.EndTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, model.EndHour.Hour, 0, 0);
            shift.ReceptionistId = model.ReceptionistId;

            await _shiftRepository.UpdateAsync(shift);
            return Result.Success();
        }
    }
}
