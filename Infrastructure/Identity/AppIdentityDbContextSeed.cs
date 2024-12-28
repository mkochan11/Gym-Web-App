using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Constants;
using Infrastructure.Helpers;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task<SeedUsersIds> SeedAsync(AppIdentityDbContext appIdentityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await EnsureRolesExist(roleManager);

            if (!await userManager.Users.AnyAsync())
            {

                var seedUsersIds = new SeedUsersIds();

                if (appIdentityDbContext.Database.IsSqlServer())
                {
                    appIdentityDbContext.Database.Migrate();
                }

                var adminUser = new ApplicationUser
                {
                    Email = "admin@gymApp.com",
                    UserName = "admin@gymApp.com" // Ensure UserName is set
                };
                var adminResult = await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_ADMIN_PASSWORD);
                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                    seedUsersIds.AdminId = (await userManager.FindByEmailAsync(adminUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Admin user: {string.Join(", ", adminResult.Errors.Select(e => e.Description))}");
                }

                // Owner User
                var ownerUser = new ApplicationUser
                {
                    Email = "owner@gymApp.com",
                    UserName = "owner@gymApp.com"
                };
                var ownerResult = await userManager.CreateAsync(ownerUser, AuthorizationConstants.DEFAULT_MANAGER_PASSWORD);
                if (ownerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(ownerUser, "Owner");
                    seedUsersIds.OwnerId = (await userManager.FindByEmailAsync(ownerUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Owner user: {string.Join(", ", ownerResult.Errors.Select(e => e.Description))}");
                }

                // Manager User
                var managerUser = new ApplicationUser
                {
                    Email = "manager@gymApp.com",
                    UserName = "manager@gymApp.com"
                };
                var managerResult = await userManager.CreateAsync(managerUser, AuthorizationConstants.DEFAULT_MANAGER_PASSWORD);
                if (managerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(managerUser, "Manager");
                    seedUsersIds.ManagerId = (await userManager.FindByEmailAsync(managerUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Manager user: {string.Join(", ", managerResult.Errors.Select(e => e.Description))}");
                }

                // Receptionist User
                var receptionistUser = new ApplicationUser
                {
                    Email = "receptionist@gymApp.com",
                    UserName = "receptionist@gymApp.com"
                };
                var receptionistResult = await userManager.CreateAsync(receptionistUser, AuthorizationConstants.DEFAULT_RECEPTIONIST_PASSWORD);
                if (receptionistResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(receptionistUser, "Receptionist");
                    seedUsersIds.ReceptionistId = (await userManager.FindByEmailAsync(receptionistUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Receptionist user: {string.Join(", ", receptionistResult.Errors.Select(e => e.Description))}");
                }

                // Group Trainer User
                var groupTrainerUser = new ApplicationUser
                {
                    Email = "grouptrainer@gymApp.com",
                    UserName = "grouptrainer@gymApp.com"
                };
                var groupTrainerResult = await userManager.CreateAsync(groupTrainerUser, AuthorizationConstants.DEFAULT_G_TRAINER_PASSWORD);
                if (groupTrainerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(groupTrainerUser, "GroupTrainer");
                    seedUsersIds.GroupTrainerId = (await userManager.FindByEmailAsync(groupTrainerUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Group Trainer user: {string.Join(", ", groupTrainerResult.Errors.Select(e => e.Description))}");
                }

                // Personal Trainer User
                var personalTrainerUser = new ApplicationUser
                {
                    Email = "personaltrainer@gymApp.com",
                    UserName = "personaltrainer@gymApp.com"
                };
                var personalTrainerResult = await userManager.CreateAsync(personalTrainerUser, AuthorizationConstants.DEFAULT_P_TRAINER_PASSWORD);
                if (personalTrainerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(personalTrainerUser, "PersonalTrainer");
                    seedUsersIds.PersonalTrainerId = (await userManager.FindByEmailAsync(personalTrainerUser.Email))?.Id;
                }
                else
                {
                    throw new Exception($"Failed to create Personal Trainer user: {string.Join(", ", personalTrainerResult.Errors.Select(e => e.Description))}");
                }

                return seedUsersIds;
            }
            else
            {
                return new SeedUsersIds();
            }
        }

        private static async Task EnsureRolesExist(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Administrator", "Owner", "Manager", "Receptionist", "GroupTrainer", "PersonalTrainer", "Client"};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
