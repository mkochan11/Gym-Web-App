using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Employee;
using ApplicationCore.Models.User;
using ApplicationCore.Services;
using Moq;

namespace UnitTests.Services
{
    public class EmployeeServiceTests
    {
        private Mock<IRepository<PersonalTrainer>> personalTrainerRepositoryMock;
        private Func<PersonalTrainer> personalTrainerFactory;
        private EmployeeService<PersonalTrainer> service;

        [SetUp]
        public void Setup()
        {
            personalTrainerRepositoryMock = new Mock<IRepository<PersonalTrainer>>();
            personalTrainerFactory = () => new PersonalTrainer
            {
                AccountId = "defaultAccountId"
            };
            service = new EmployeeService<PersonalTrainer>(personalTrainerRepositoryMock.Object, personalTrainerFactory);
        }

        [Test]
        public async Task AddPersonalTrainerShouldReturnSuccess()
        {
            var newTrainerModel = new NewEmployeeModel
            {
                AccountId = "defaultAccountId",
                Name = "Alice",
                Surname = "Johnson",
                Position = ApplicationCore.Enums.Position.PersonalTrainer,
                Salary = 4000,
                EmploymentDate = DateTime.Now.AddDays(-30)
            };

            personalTrainerRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<PersonalTrainer>(), default))
                .ReturnsAsync(new PersonalTrainer { AccountId = "defaultAccountId" });

            var result = await service.AddEmployee(newTrainerModel);

            personalTrainerRepositoryMock.Verify(x => x.AddAsync(It.Is<PersonalTrainer>(
                t => t.AccountId == newTrainerModel.AccountId &&
                     t.Name == newTrainerModel.Name &&
                     t.Surname == newTrainerModel.Surname &&
                     t.Position == newTrainerModel.Position &&
                     t.Salary == newTrainerModel.Salary &&
                     t.EmploymentDate == newTrainerModel.EmploymentDate), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task AddPersonalTrainerShouldReturnErrorOnFailure()
        {
            var newTrainerModel = new NewEmployeeModel
            {
                AccountId = "defaultAccountId",
                Name = "Alice",
                Surname = "Johnson",
                Position = ApplicationCore.Enums.Position.PersonalTrainer,
                Salary = 4000,
                EmploymentDate = DateTime.Now.AddDays(-30)
            };

            personalTrainerRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<PersonalTrainer>(), default))
                .ReturnsAsync((PersonalTrainer)null);

            var result = await service.AddEmployee(newTrainerModel);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Wystąpił błąd podczas dodawania użytkownika."));
        }

        [Test]
        public async Task DeletePersonalTrainerShouldReturnSuccess()
        {
            var trainer = new PersonalTrainer 
            { 
                AccountId = "defaultAccountId", 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync(trainer);

            personalTrainerRepositoryMock
                .Setup(x => x.DeleteAsync(trainer, default))
                .Verifiable();

            var result = await service.DeleteEmployee(1);

            personalTrainerRepositoryMock
                .Verify(x => x.DeleteAsync(trainer, default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task DeletePersonalTrainerShouldReturnErrorIfNotFound()
        {
            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync((PersonalTrainer)null);

            var result = await service.DeleteEmployee(1);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Nie znaleziono pracownika."));
        }

        [Test]
        public async Task GetPersonalTrainerAccountIdShouldReturnAccountId()
        {
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "trainer1" 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync(trainer);

            var result = await service.GetEmployeeAccountId(1);

            Assert.AreEqual("trainer1", result);
        }

        [Test]
        public async Task GetPersonalTrainerAccountIdShouldReturnEmptyStringIfNotFound()
        {
            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync((PersonalTrainer)null);

            var result = await service.GetEmployeeAccountId(1);

            Assert.AreEqual("", result);
        }

        [Test]
        public async Task UpdatePersonalTrainerShouldReturnSuccess()
        {
            var editTrainerModel = new EditEmployeeModel
            {
                Id = 1,
                Name = "Bob",
                Surname = "Smith",
                Position = ApplicationCore.Enums.Position.PersonalTrainer,
                Salary = 5000,
                EmploymentDate = DateTime.Now.AddDays(-20)
            };
            var trainer = new PersonalTrainer 
            {
                AccountId = "trainer1",
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync(trainer);

            personalTrainerRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<PersonalTrainer>(), default))
                .Verifiable();

            var result = await service.UpdateEmployee(editTrainerModel);

            personalTrainerRepositoryMock
                .Verify(x => x.UpdateAsync(It.Is<PersonalTrainer>(
                    t => t.Id == editTrainerModel.Id &&
                     t.Name == editTrainerModel.Name &&
                     t.Surname == editTrainerModel.Surname &&
                     t.Position == editTrainerModel.Position &&
                     t.Salary == editTrainerModel.Salary &&
                     t.EmploymentDate == editTrainerModel.EmploymentDate), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task UpdatePersonalTrainerShouldReturnErrorIfNotFound()
        {
            var editTrainerModel = new EditEmployeeModel 
            {
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.GetByIdAsync(1, default))
                .ReturnsAsync((PersonalTrainer)null);

            var result = await service.UpdateEmployee(editTrainerModel);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Nie znaleziono pracownika."));
        }
    }
}