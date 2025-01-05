
using ApplicationCore.Models.Shift;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IShiftService
    {
        Task<Result> CreateShift(NewShiftModel model);
        Task<Result> UpdateShift(EditShiftModel model);
        Task<Result> DeleteShift(int shiftId);
    }
}
