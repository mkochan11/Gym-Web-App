using ApplicationCore.Models.Training;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGroupTrainingService
    {
        Task<Result> ReservePlace(int trainingId, string userId);
        Task<Result> CancelPlace(int trainingId, string userId);
        Task<Result> CreateTraining(NewGroupTrainingModel model, string userId);
        Task<Result> DeleteTraining(int trainingId, string userId);
        Task<Result> UpdateTraining(EditGroupTrainingModel model, string userId);
    }
}
