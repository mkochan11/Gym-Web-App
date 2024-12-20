using ApplicationCore.Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext appDbContext, SeedUsersIds ids)
        {
            try
            {
                if (appDbContext.Database.IsSqlServer())
                {
                    appDbContext.Database.Migrate();
                }


                if (!await appDbContext.Employees.AnyAsync())
                {
                    await appDbContext.Employees.AddRangeAsync(
                        GetPreconfiguredEmployees(ids));

                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.MembershipPlans.AnyAsync())
                {
                    await appDbContext.MembershipPlans.AddRangeAsync(
                        GetPreconfiguredMembershipPlans());

                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.TrainingTypes.AnyAsync())
                {
                    await appDbContext.TrainingTypes.AddRangeAsync(
                        GetPreconfiguredTrainingTypes());

                    await appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        static IEnumerable<MembershipPlan> GetPreconfiguredMembershipPlans()
        {
            return new List<MembershipPlan>
            {
                new MembershipPlan
                {
                    Type = "Klasyczny",
                    Description = "Klasyczny karnet",
                    Price = 150.00m,
                    DurationTime = "3 Months"
                },
                new MembershipPlan
                {
                    Type = "Premium",
                    Description = "Karnet premium",
                    Price = 250.00m,
                    DurationTime = "3 Months"
                },
                new MembershipPlan
                {
                    Type = "Student",
                    Description = "Karnet studencki",
                    Price = 100.00m,
                    DurationTime = "3 Months"
                }
            };
        }

        static IEnumerable<TrainingType> GetPreconfiguredTrainingTypes()
        {
            return new List<TrainingType>
            {
                new TrainingType
                {
                    Name = "Crossfit",
                    Description = "Zajęcia Crossfitowe"
                },
                new TrainingType
                {
                    Name = "Sztangi",
                    Description = "Zajęcia ze sztangami"
                },
                new TrainingType
                {
                    Name = "Fitness",
                    Description = "Zajęcia Fitnessowe"
                },
                new TrainingType
                {
                    Name = "Taniec",
                    Description = "Zajęcia taneczne"
                },
            };
        }

        static IEnumerable<Employee> GetPreconfiguredEmployees(SeedUsersIds ids)
        {
            return new List<Employee>
            {
                new Employee
                {
                    AccountId = ids.AdminId,
                    Name = "Admin",
                    Surname = "Admin",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.Admin,
                    EmploymentDate = DateTime.Now,
                    Salary = 30.00m,
                },
                new Employee
                {
                    AccountId = ids.OwnerId,
                    Name = "Michał",
                    Surname = "Kochanowski",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.Owner,
                    EmploymentDate = DateTime.Now,
                    Salary = 100.00m,
                },
                new Employee
                {
                    AccountId = ids.ManagerId,
                    Name = "Joe",
                    Surname = "Manager",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.Manager,
                    EmploymentDate = DateTime.Now,
                    Salary = 70.00m,
                },
                new Employee
                {
                    AccountId = ids.ReceptionistId,
                    Name = "Jan",
                    Surname = "Kowalski",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.Receptionist,
                    EmploymentDate = DateTime.Now,
                    Salary = 30.00m,
                },
                new Employee
                {
                    AccountId = ids.GroupTrainerId,
                    Name = "Anna",
                    Surname = "Grupowska",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.GroupTrainer,
                    EmploymentDate = DateTime.Now,
                    Salary = 50.00m,
                },
                new Employee
                {
                    AccountId = ids.PersonalTrainerId,
                    Name = "Adam",
                    Surname = "Personalny",
                    RegistrationDate = DateTime.Now,
                    Position = ApplicationCore.Enums.Position.PersonalTrainer,
                    EmploymentDate = DateTime.Now,
                    Salary = 40.00m,
                }
            };
        }
    }
}
