using ApplicationCore.Models.Exercise;
using ApplicationCore.Models.TrainingPlan;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface ITrainingPlanService
    {
        Task<Result> CreateTrainingPlan(NewTrainingPlanModel model, string userId);
        Task<Result> DeletePlan(int planId, string userId);
        Task<Result> UpdateTrainingPlan(EditTrainingPlanModel model, string userId);
        Task<Result> AddNewExercise(NewExerciseModel model, string userId);
        Task<Result> EditExercise(EditExerciseModel model, string userId);
        Task<Result> DeleteExercise(int exerciseId, string userId);
    }
}
