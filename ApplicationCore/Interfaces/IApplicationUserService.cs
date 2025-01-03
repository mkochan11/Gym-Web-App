
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IApplicationUserService
    {
        Task<Result> DeleteUser(string accountId);
        Task<Result> UpdateUser(string accountId);
    }
}
