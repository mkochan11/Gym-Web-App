using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
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
                if (!await appDbContext.Owners.AnyAsync())
                {
                    var owner = GetPreconfiguredOwner(ids);
                    await appDbContext.Owners.AddAsync(owner);
                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.Managers.AnyAsync())
                {
                    var manager = GetPreconfiguredManager(ids);
                    await appDbContext.Managers.AddAsync(manager);
                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.GroupTrainers.AnyAsync())
                {
                    var groupTrainer = GetPreconfiguredGroupTrainer(ids);
                    await appDbContext.GroupTrainers.AddAsync(groupTrainer);
                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.PersonalTrainers.AnyAsync())
                {
                    var personalTrainer = GetPreconfiguredPersonalTrainer(ids);
                    await appDbContext.PersonalTrainers.AddAsync(personalTrainer);
                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.Receptionists.AnyAsync())
                {
                    var receptionist = GetPreconfiguredReceptionist(ids);
                    await appDbContext.Receptionists.AddAsync(receptionist);
                    await appDbContext.SaveChangesAsync();
                }

                if (!await appDbContext.GroupTrainings.AnyAsync())
                {
                    var groupTrainer = await appDbContext.GroupTrainers.FirstOrDefaultAsync();
                    var trainingTypes = await appDbContext.TrainingTypes.ToListAsync();

                    if (groupTrainer != null && trainingTypes.Any())
                    {
                        var groupTrainings = GetPreconfiguredGroupTrainings(groupTrainer, trainingTypes);
                        await appDbContext.GroupTrainings.AddRangeAsync(groupTrainings);
                        await appDbContext.SaveChangesAsync();
                    }
                }

                if (!await appDbContext.IndividualTrainings.AnyAsync())
                {
                    var personalTrainer = await appDbContext.PersonalTrainers.FirstOrDefaultAsync();

                    if (personalTrainer != null)
                    {
                        var individualTrainings = GetPreconfiguredIndividualTrainings(personalTrainer);
                        await appDbContext.IndividualTrainings.AddRangeAsync(individualTrainings);
                        await appDbContext.SaveChangesAsync();
                    }
                }

                if (!await appDbContext.ReceptionistsShifts.AnyAsync())
                {
                    var receptionist = await appDbContext.Receptionists.FirstOrDefaultAsync();

                    if (receptionist != null)
                    {
                        var receptionistShifts = GetPreconfiguredReceptionistShift(receptionist);
                        await appDbContext.ReceptionistsShifts.AddRangeAsync(receptionistShifts);
                        await appDbContext.SaveChangesAsync();
                    }
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
                    Description = "Dostęp do siłowni w godzinach otwarcia siłowni, bez dostępu do zajęć grupowych i indywidualnych.",
                    Price = 150.00m,
                    DurationTime = "3 Miesiące",
                    DurationInMonths = 3                    
                },
                new MembershipPlan
                {
                    Type = "Premium",
                    Description = "Dostęp do siłowni oraz zajęć indywiudalnych oraz grupowych w godzinach otwarcia siłowni.",
                    Price = 600.00m,
                    DurationTime = "6 Miesięcy",
                    DurationInMonths = 6
                },
                new MembershipPlan
                {
                    Type = "Studencki",
                    Description = "Dostęp do siłowni w godzinach 7-9 oraz 16-19, bez dostępu do zajęć grupowych i indywidualnych.",
                    Price = 100.00m,
                    DurationTime = "3 Miesiące",
                    DurationInMonths = 3                    
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


        static Owner GetPreconfiguredOwner(SeedUsersIds ids)
        {
            return new Owner
            {
                AccountId = ids.OwnerId,
                Name = "Michał",
                Surname = "Kochanowski",
                RegistrationDate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Position = ApplicationCore.Enums.Position.Owner,
                Salary = 200.00m
            };
        }
        static Manager GetPreconfiguredManager(SeedUsersIds ids)
        {
            return new Manager
            {
                AccountId = ids.ManagerId,
                Name = "Adam",
                Surname = "Kowalski",
                RegistrationDate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Position = ApplicationCore.Enums.Position.Manager,
                Salary = 100.00m
            };
        }
        static GroupTrainer GetPreconfiguredGroupTrainer(SeedUsersIds ids)
        {
            return new GroupTrainer
            {
                AccountId = ids.GroupTrainerId,
                Name = "Jan",
                Surname = "Grupowy",
                RegistrationDate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Position = ApplicationCore.Enums.Position.GroupTrainer,
                Salary = 75.00m
            };
        }
        static PersonalTrainer GetPreconfiguredPersonalTrainer(SeedUsersIds ids)
        {
            return new PersonalTrainer
            {
                AccountId = ids.PersonalTrainerId,
                Name = "Maciej",
                Surname = "Indywidualny",
                RegistrationDate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Position = ApplicationCore.Enums.Position.GroupTrainer,
                Salary = 80.00m
            };
        }
        static Receptionist GetPreconfiguredReceptionist(SeedUsersIds ids)
        {
            return new Receptionist
            {
                AccountId = ids.ReceptionistId,
                Name = "Anna",
                Surname = "Recepjowa",
                RegistrationDate = DateTime.Now,
                EmploymentDate = DateTime.Now,
                Position = ApplicationCore.Enums.Position.Receptionist,
                Salary = 40.00m
            };
        }
        static IEnumerable<GroupTraining> GetPreconfiguredGroupTrainings(GroupTrainer trainer, List<TrainingType> types)
        {
            return new List<GroupTraining>
            {
                new GroupTraining
                {
                    Date = new DateTime(2025, 1, 15, 17, 0, 0),
                    Duration = TimeSpan.FromMinutes(60),
                    Description = "Trening grupowy " + types.ElementAt(0).Description,
                    GroupTrainer = trainer,
                    MaxParticipantNumber = 20,
                    TrainingType = types.ElementAt(0)
                },
                new GroupTraining
                {
                    Date = new DateTime(2025, 1, 16, 18, 0, 0),
                    Duration = TimeSpan.FromMinutes(45),
                    Description = "Trening grupowy " + types.ElementAt(1).Description,
                    GroupTrainer = trainer,
                    MaxParticipantNumber = 30,
                    TrainingType = types.ElementAt(1)
                },
                new GroupTraining
                {
                    Date = new DateTime(2025, 1, 15, 20, 30, 0),
                    Duration = TimeSpan.FromMinutes(30),
                    Description = "Trening grupowy " + types.ElementAt(2).Description,
                    GroupTrainer = trainer,
                    MaxParticipantNumber = 15,
                    TrainingType = types.ElementAt(2)
                }
            };
        }
        static IEnumerable<IndividualTraining> GetPreconfiguredIndividualTrainings(PersonalTrainer trainer)
        {
            return new List<IndividualTraining>
            {
                new IndividualTraining
                {
                    Date = new DateTime(2025, 1, 15, 13, 0, 0),
                    Duration = TimeSpan.FromMinutes(60),
                    PersonalTrainer = trainer,
                },
                new IndividualTraining
                {
                    Date = new DateTime(2025, 1, 14, 15, 0, 0),
                    Duration = TimeSpan.FromMinutes(30),
                    PersonalTrainer = trainer,
                },
                new IndividualTraining
                {
                    Date = new DateTime(2025, 1, 18, 19, 15, 0),
                    Duration = TimeSpan.FromMinutes(45),
                    PersonalTrainer = trainer,
                }
            };
        }

        static IEnumerable<Shift<Receptionist>> GetPreconfiguredReceptionistShift(Receptionist receptionist)
        {
            return new List<Shift<Receptionist>>
            {
                new Shift<Receptionist>
                {
                    StartTime = new DateTime(2025, 1, 15, 7, 0, 0),
                    EndTime = new DateTime(2025, 1, 15, 23, 0, 0),
                    Employee = receptionist,
                },
                new Shift<Receptionist>
                {
                    StartTime = new DateTime(2025, 1, 16, 15, 0, 0),
                    EndTime = new DateTime(2025, 1, 16, 23, 0, 0),
                    Employee = receptionist,
                },
                new Shift<Receptionist>
                {
                    StartTime = new DateTime(2025, 1, 16, 7, 0, 0),
                    EndTime = new DateTime(2025, 1, 16, 15, 0, 0),
                    Employee = receptionist,
                }
            };
        }
    }
}
