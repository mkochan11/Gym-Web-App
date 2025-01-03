
using ApplicationCore.Interfaces;
using Ardalis.Result;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<Result> AddUser()
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteUser(string accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateUser(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}
