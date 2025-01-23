using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Membership;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Moq;

namespace UnitTests.Services
{
    public class MembershipServiceTests
    {
        private Mock<IRepository<GymMembership>> membershipRepositoryMock;
        private Mock<IRepository<MembershipPlan>> membershipPlanRepositoryMock;
        private Mock<IRepository<Client>> clientRepositoryMock;
        private Mock<IRepository<Payment>> paymentRepositoryMock;
        private MembershipService service;

        [SetUp]
        public void Setup()
        {
            membershipRepositoryMock = new Mock<IRepository<GymMembership>>();
            membershipPlanRepositoryMock = new Mock<IRepository<MembershipPlan>>();
            clientRepositoryMock = new Mock<IRepository<Client>>();
            paymentRepositoryMock = new Mock<IRepository<Payment>>();

            service = new MembershipService(
                membershipRepositoryMock.Object,
                membershipPlanRepositoryMock.Object,
                clientRepositoryMock.Object,
                paymentRepositoryMock.Object
            );
        }

        [Test]
        public async Task GetActiveMembershipShouldReturnActiveMembership()
        {
            int clientId = 1;

            var memberships = new List<GymMembership>
            {
                new GymMembership 
                { 
                    StartDate = DateTime.Now.AddMonths(-2), 
                    EndDate = DateTime.Now.AddMonths(1) 
                },

                new GymMembership 
                { 
                    StartDate = DateTime.Now.AddYears(-1), 
                    EndDate = DateTime.Now.AddMonths(-1) 
                }
            };

            membershipRepositoryMock
                .Setup(x => x.ListAsync(It.IsAny<FindMembershipByClientId>(), default))
                .ReturnsAsync(memberships);

            var result = await service.GetActiveMembership(clientId);

            Assert.IsNotNull(result);
            Assert.IsTrue(service.IsMembershipActive(result));
        }

        [Test]
        public async Task GetActiveMembershipShouldReturnNullWhenNoActiveMembership()
        {
            int clientId = 1;

            var memberships = new List<GymMembership>
            {
                new GymMembership 
                { 
                    StartDate = DateTime.Now.AddYears(-2), 
                    EndDate = DateTime.Now.AddMonths(-1) 
                },
                new GymMembership 
                { 
                    StartDate = DateTime.Now.AddYears(-1), 
                    EndDate = DateTime.Now.AddMonths(-6) 
                }
            };

            membershipRepositoryMock
                .Setup(x => x.ListAsync(It.IsAny<FindMembershipByClientId>(), default))
                .ReturnsAsync(memberships);

            var result = await service.GetActiveMembership(clientId);

            Assert.IsNull(result);
        }

        [Test]
        public void IsMembershipActiveShouldReturnTrueForActiveMembership()
        {
            var membership = new GymMembership
            {
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now.AddMonths(1)
            };

            var result = service.IsMembershipActive(membership);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsMembershipActiveShouldReturnFalseForInactiveMembership()
        {
            var membership = new GymMembership
            {
                StartDate = DateTime.Now.AddYears(-1),
                EndDate = DateTime.Now.AddMonths(-1)
            };

            var result = service.IsMembershipActive(membership);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddGymMembershipShouldReturnSuccess()
        {
            var newMembershipModel = new NewMembershipModel
            {
                UserId = "testUserId",
                MembershipPlanId = 1,
                PaymentMethod = "Karta"
            };

            var client = new Client 
            { 
                Id = 1, 
                AccountId = newMembershipModel.UserId 
            };

            var membershipPlan = new MembershipPlan 
            { 
                Id = 1, 
                DurationInMonths = 6 
            };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            membershipPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(newMembershipModel.MembershipPlanId, default))
                .ReturnsAsync(membershipPlan);

            membershipRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<GymMembership>(), default))
                .ReturnsAsync(new GymMembership());

            paymentRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Payment>(), default))
                .ReturnsAsync(new Payment());

            var result = await service.AddGymMembership(newMembershipModel);

            clientRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default), Times.Once);

            membershipPlanRepositoryMock
                .Verify(x => x.GetByIdAsync(newMembershipModel.MembershipPlanId, default), Times.Once);

            membershipRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<GymMembership>(), default), Times.Once);

            paymentRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<Payment>(), default), Times.Once);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task AddGymMembershipShouldReturnErrorWhenClientNotFound()
        {
            var newMembershipModel = new NewMembershipModel
            {
                UserId = "testUserId",
                MembershipPlanId = 1,
                PaymentMethod = "CreditCard"
            };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync((Client)null);

            var result = await service.AddGymMembership(newMembershipModel);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Errors.First(), Is.EqualTo("Client not found"));
        }

        [Test]
        public async Task AddGymMembershipShouldReturnErrorWhenMembershipPlanNotFound()
        {
            var newMembershipModel = new NewMembershipModel
            {
                UserId = "testUserId",
                MembershipPlanId = 1,
                PaymentMethod = "CreditCard"
            };

            var client = new Client { Id = 1, AccountId = newMembershipModel.UserId };

            clientRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindClientByUserId>(), default))
                .ReturnsAsync(client);

            membershipPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(newMembershipModel.MembershipPlanId, default))
                .ReturnsAsync((MembershipPlan)null);

            var result = await service.AddGymMembership(newMembershipModel);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.Errors.First(), Is.EqualTo("Membership plan not found"));
        }
    }
}