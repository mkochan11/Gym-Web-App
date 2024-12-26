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
    }
}
