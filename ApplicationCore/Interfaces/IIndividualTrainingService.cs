using ApplicationCore.Models.Training;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IIndividualTrainingService
    {
        Task<Result> Reserve(int trainingId, string userId);
        Task<Result> CancelReservation(int trainingId, string userId);
        Task<Result> CreateTraining(NewIndividualTrainingModel model, string userId);
        Task<Result> DeleteTraining(int trainingId, string userId);
        Task<Result> UpdateTraining(EditIndividualTrainingModel model, string userId);
    }
}
