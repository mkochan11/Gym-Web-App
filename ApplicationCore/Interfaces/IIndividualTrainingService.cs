using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IIndividualTrainingService
    {
        Task<Result> Reserve(int trainingId, string userId);
        Task<Result> CancelReservation(int trainingId, string userId);
    }
}
