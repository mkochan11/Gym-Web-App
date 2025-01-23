using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Training;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Moq;

namespace UnitTests.Services
{
    public class IndividualTrainingServiceTests
    {
        private Mock<IRepository<Client>> clientRepositoryMock;
        private Mock<IRepository<GymMembership>> gymMembershipRepositoryMock;
        private Mock<IRepository<MembershipPlan>> membershipPlanRepositoryMock;
        private Mock<IRepository<IndividualTraining>> individualTrainingRepositoryMock;
        private Mock<IRepository<PersonalTrainer>> personalTrainerRepositoryMock;
        private Mock<IMembershipService> membershipServiceMock;
        private IndividualTrainingService service;

        [SetUp]
        public void Setup()
        {
            clientRepositoryMock = new Mock<IRepository<Client>>();
            gymMembershipRepositoryMock = new Mock<IRepository<GymMembership>>();
            membershipPlanRepositoryMock = new Mock<IRepository<MembershipPlan>>();
            individualTrainingRepositoryMock = new Mock<IRepository<IndividualTraining>>();
            personalTrainerRepositoryMock = new Mock<IRepository<PersonalTrainer>>();
            membershipServiceMock = new Mock<IMembershipService>();

            service = new IndividualTrainingService(
                clientRepositoryMock.Object,
                gymMembershipRepositoryMock.Object,
                membershipPlanRepositoryMock.Object,
                individualTrainingRepositoryMock.Object,
                personalTrainerRepositoryMock.Object,
                membershipServiceMock.Object);
        }

        [Test]
        public async Task CancelReservationShouldReturnSuccess()
        {
            int trainingId = 1;
            string userId = "testUser";

            var client = new Client { Id = 1, AccountId = userId };
            var training = new IndividualTraining { Id = trainingId, ClientId = client.Id };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            individualTrainingRepositoryMock
                .Setup(x => x.GetByIdAsync(trainingId, default))
                .ReturnsAsync(training);

            individualTrainingRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<IndividualTraining>(), default))
                .Verifiable();

            var result = await service.CancelReservation(trainingId, userId);

            individualTrainingRepositoryMock
                .Verify(x => x.UpdateAsync(It.Is<IndividualTraining>(t => t.ClientId == null), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task CancelReservationShouldReturnClientNotFound()
        {
            string userId = "testUser";

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(new FindClientByUserId(userId), default))
                .ReturnsAsync((Client)null);

            var result = await service.CancelReservation(1, userId);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Nie znaleziono klienta"));
        }

        [Test]
        public async Task CancelReservationShouldReturnTrainingNotFound()
        {
            int trainingId = 1;
            string userId = "testUser";

            var client = new Client 
            { 
                Id = 1,
                AccountId = userId 
            };

            clientRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            individualTrainingRepositoryMock.Setup(x => x.GetByIdAsync(trainingId, default))
                .ReturnsAsync((IndividualTraining)null);

            var result = await service.CancelReservation(trainingId, userId);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Nie znaleziono treningu"));
        }

        [Test]
        public async Task ReserveShouldReturnSuccess()
        {
            int trainingId = 1;
            string userId = "testUser";

            var client = new Client 
            { 
                Id = 1, 
                AccountId = userId 
            };

            var membership = new GymMembership 
            { 
                Id = 1, 
                MembershipPlanId = 1
            };

            var membershipPlan = new MembershipPlan 
            { 
                Id = 1, 
                CanReserveTrainings = true 
            };

            var training = new IndividualTraining 
            { 
                Id = trainingId, 
                ClientId = null 
            };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            membershipServiceMock
                .Setup(x => x.GetActiveMembership(client.Id))
                .ReturnsAsync(membership);

            membershipPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(membership.MembershipPlanId, default))
                .ReturnsAsync(membershipPlan);

            individualTrainingRepositoryMock
                .Setup(x => x.GetByIdAsync(trainingId, default))
                .ReturnsAsync(training);

            individualTrainingRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<IndividualTraining>(), default))
                .Verifiable();

            var result = await service.Reserve(trainingId, userId);

            individualTrainingRepositoryMock
                .Verify(x => x.UpdateAsync(It.Is<IndividualTraining>(t => t.ClientId == client.Id), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task ReserveShouldReturnTrainingAlreadyReserved()
        {
            int trainingId = 1;
            string userId = "testUser";
            string userId2 = "testUser2";

            var client = new Client 
            { 
                Id = 1, 
                AccountId = userId 
            };

            var client2 = new Client
            {
                Id = 2,
                AccountId = userId2
            };

            var training = new IndividualTraining 
            { 
                Id = trainingId, 
                ClientId = 2 
            };

            var membership = new GymMembership
            {
                Id = 1,
                MembershipPlanId = 1
            };

            var membershipPlan = new MembershipPlan
            {
                Id = 1,
                CanReserveTrainings = true
            };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            membershipServiceMock
                .Setup(x => x.GetActiveMembership(client.Id))
                .ReturnsAsync(membership);

            membershipPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(membership.MembershipPlanId, default))
                .ReturnsAsync(membershipPlan);

            individualTrainingRepositoryMock
                .Setup(x => x.GetByIdAsync(trainingId, default))
                .ReturnsAsync(training);

            clientRepositoryMock
                .Setup(x => x.GetByIdAsync(training.ClientId, default))
                .ReturnsAsync(client2);

            var result = await service.Reserve(trainingId, userId);

            Assert.False(result.IsSuccess);
            Assert.That(result.Errors.First().ToString(), Is.EqualTo("Trening jest już zarezerwowany"));
        }

        [Test]
        public async Task CreateTrainingShouldReturnSuccessForSingleTraining()
        {
            string userId = "trainerUser";
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = userId 
            };

            var newTrainingModel = new NewIndividualTrainingModel
            {
                Date = DateTime.Now,
                Hour = DateTime.Now,
                Duration = new TimeSpan(1, 0, 0),
                Description = "Test training",
                IsCyclic = false,
                Repeatability = ""
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            individualTrainingRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<IndividualTraining>(), default))
                .Verifiable();

            var result = await service.CreateTraining(newTrainingModel, userId);

            individualTrainingRepositoryMock
                .Verify(x => x.AddAsync(It.Is<IndividualTraining>(
                    t => t.PersonalTrainerId == trainer.Id &&
                    t.Description == newTrainingModel.Description), default), Times.Once);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task DeleteTrainingShouldReturnSuccess()
        {
            int trainingId = 1;
            string userId = "trainerUser";
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = userId 
            };

            var training = new IndividualTraining 
            { 
                Id = trainingId 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            individualTrainingRepositoryMock
                .Setup(x => x.GetByIdAsync(trainingId, default))
                .ReturnsAsync(training);

            individualTrainingRepositoryMock
                .Setup(x => x.DeleteAsync(training, default))
                .Verifiable();

            var result = await service.DeleteTraining(trainingId, userId);

            individualTrainingRepositoryMock.Verify(x => x.DeleteAsync(training, default), Times.Once);
            Assert.True(result.IsSuccess);
        }

        [Test]
        public async Task UpdateTrainingShouldReturnSuccess()
        {
            string userId = "trainerUser";
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = userId 
            };

            var editModel = new EditIndividualTrainingModel
            {
                Id = 1,
                Date = DateTime.Now.AddDays(1),
                Hour = DateTime.Now.AddDays(1).AddHours(1),
                Duration = new TimeSpan(1, 0, 0),
                Description = "Updated training"
            };
            var training = new IndividualTraining { Id = editModel.Id };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            individualTrainingRepositoryMock
                .Setup(x => x.GetByIdAsync(editModel.Id, default))
                .ReturnsAsync(training);

            individualTrainingRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<IndividualTraining>(), default))
                .Verifiable();

            var result = await service.UpdateTraining(editModel, userId);

            individualTrainingRepositoryMock.Verify(x => x.UpdateAsync(It.Is<IndividualTraining>(
                t => t.Id == editModel.Id &&
                     t.Date == editModel.Date.AddHours(editModel.Hour.Hour) &&
                     t.Duration == editModel.Duration &&
                     t.Description == editModel.Description), default), Times.Once);

            Assert.True(result.IsSuccess);
        }
    }
}