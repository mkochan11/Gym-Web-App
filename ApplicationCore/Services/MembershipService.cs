using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.Result;
using ApplicationCore.Models.Membership;
using ApplicationCore.Enums;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepository<GymMembership> _membershipRepository;
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Payment> _paymentRepository;

        public MembershipService(
            IRepository<GymMembership> membershipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<Client> clientRepository,
            IRepository<Payment> paymentRepository
            )
        {
            _membershipRepository = membershipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _clientRepository = clientRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<GymMembership> GetActiveMembership(int clientId)
        {
            var _membershipSpec = new FindMembershipByClientId(clientId);
            var clientMemberships = await _membershipRepository.ListAsync(_membershipSpec);

            GymMembership activeMembership = null;

            foreach (var clientMembership in clientMemberships)
            {
                if (IsMembershipActive(clientMembership))
                {
                    activeMembership = clientMembership;
                }
            }
            return activeMembership;
        }

        public bool IsMembershipActive(GymMembership membership) {
            DateTime currentDate = DateTime.Now;
            return membership.StartDate < currentDate && membership.EndDate > currentDate;
        }

        public async Task<Result> AddGymMembership(NewMembershipModel model)
        {
            var _clientSpec = new FindClientByUserId(model.UserId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if(client == null)
            {
                return Result.Error("Client not found");
            }

            var membershipPlan = await _membershipPlanRepository.GetByIdAsync(model.MembershipPlanId);

            if(membershipPlan == null)
            {
                return Result.Error("Membership plan not found");
            }

            GymMembership membership = new GymMembership{
                Client = client,
                MembershipPlan = membershipPlan,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(membershipPlan.DurationInMonths),
            };

            var result = await _membershipRepository.AddAsync(membership);
            
            if(result == null){
                return Result.Error("Membership could not be added");
            }

            
            if (!Enum.TryParse(model.PaymentMethod, out PaymentMethod method)) {
                return Result.Error("Could not resolve payment method");
            }

            PaymentMethod paymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), model.PaymentMethod);

            Payment payment = new Payment
            {
                GymMembership = membership,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentMethod,
            };

            var paymentResult = await _paymentRepository.AddAsync(payment);

            if (paymentResult == null)
            {
                return Result.Error("Payment could not be added");
            }

            return Result.Success();
        }
    }
}