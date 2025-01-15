using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
using ApplicationCore.Services;
using Moq;

namespace UnitTests.Services
{
    public class ClientServiceTests
    {
        private Mock<IRepository<Client>> clientRepositoryMock;
        private Mock<IMembershipService> membershipServiceMock;
        private ClientService service;

        [SetUp]
        public void Setup()
        {
            clientRepositoryMock = new Mock<IRepository<Client>>();
            membershipServiceMock = new Mock<IMembershipService>();

            service = new ClientService(clientRepositoryMock.Object, membershipServiceMock.Object);
        }

        [Test]
        public async Task AddClientShouldReturnSuccess()
        {
            var newClientModel = new NewClientModel
            {
                AccountId = "testId",
                Name = "testName",
                Surname = "testSurname"
            };

            var registrationDate = DateTime.Now;

            var newClient = new Client
            {
                AccountId = "testId",
                Name = "testName",
                Surname = "testSurname",
                RegistrationDate = registrationDate,
            };

            clientRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Client>(), default))
                .ReturnsAsync(newClient);   

            var result = await service.AddClient(newClientModel);

            clientRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Client>(), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task UpdateClientShouldReturnSuccess()
        {
            int id = 1;

            var editClientModel = new EditClientModel
            {
                Id = id,
                Name = "testName",
                Surname = "testSurname"
            };

            var oldClient = new Client
            {
                Id = id,
                AccountId = "oldAccountId",
                Name = "oldTestName",
                Surname = "oldTestSurname"
            };

            var newClient = new Client
            {
                AccountId = "oldAccountId",
                Name = "testName",
                Surname = "testSurname",
            };

            clientRepositoryMock
                .Setup(x => x.GetByIdAsync(id, default))
                .ReturnsAsync(oldClient);

            clientRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Client>(), default))
                .Verifiable();

            var result = await service.UpdateClient(editClientModel);

            clientRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>(), default), Times.Once);

            clientRepositoryMock
                .Verify(x => x.UpdateAsync(It.Is<Client>(y => y.Name == newClient.Name && y.Surname == newClient.Surname), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task UpdateClientShouldReturnClientNotFound()
        {
            int id = 1;

            var editClientModel = new EditClientModel
            {
                Id = id,
                Name = "testName",
                Surname = "testSurname"
            };

            var oldClient = new Client
            {
                Id = 2,
                AccountId = "oldAccountId",
                Name = "oldTestName",
                Surname = "oldTestSurname"
            };

            var newClient = new Client
            {
                AccountId = "oldAccountId",
                Name = "testName",
                Surname = "testSurname",
            };

            clientRepositoryMock
                .Setup(x => x.GetByIdAsync(id, default))
                .ReturnsAsync((Client)null);

            clientRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Client>(), default))
                .Verifiable();

            var result = await service.UpdateClient(editClientModel);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Nie znaleziono klienta"));
        }
    }
}
