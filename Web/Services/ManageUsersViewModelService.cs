using ApplicationCore.Entities.Abstract;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Web.Interfaces;
using Web.ViewModels.Admin.ManageUsers;

namespace Web.Services
{
    public class ManageUsersViewModelService<T> : IManageUsersViewModelService<T> where T : User
    {
        private readonly IRepository<T> _usersRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersViewModelService(IRepository<T> usersRepository, UserManager<ApplicationUser> userManager)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
        }
        public async Task<ManageUsersIndexViewModel> GetManageUsersIndexViewModel()
        {
            var users = await _usersRepository.ListAsync();

            var userItems = new List<ManageUsersIndexItemViewModel>();

            foreach (var user in users)
            {
                var appUser = await _userManager.FindByIdAsync(user.AccountId);

                userItems.Add(new ManageUsersIndexItemViewModel
                {
                    Id = user.Id,
                    UserId = user.AccountId,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = appUser is null ? "Nie znaleziono adresu e-mail" : appUser.Email,
                    CreatedDate = user.RegistrationDate,
                });
            }

            var viewModel = new ManageUsersIndexViewModel
            {
                Users = userItems,
            };

            return viewModel;
        }
    }
}
