using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class IndividualTrainingService : IIndividualTrainingService
    {
        IRepository<Client> _clientRepository;
        IRepository<GymMembership> _gymMembershipRepository;
        IRepository<MembershipPlan> _membershipPlanRepository;
        IRepository<IndividualTraining> _individualTrainingRepository;
        IMembershipService _membershipService;

        public IndividualTrainingService(
            IRepository<Client> clientRepository,
            IRepository<GymMembership> gymMemberhsipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<IndividualTraining> individualTrainingRepository,
            IMembershipService membershipService
            )
        {
            _clientRepository = clientRepository;
            _gymMembershipRepository = gymMemberhsipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _individualTrainingRepository = individualTrainingRepository;
            _membershipService = membershipService;
        }

        public async Task<Result> CancelReservation(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            if (training.ClientId != null && training.ClientId != client.Id)
            {
                return Result.Error("Trening został zarezerwowany przez innego klienta");
            }
            else if (training.ClientId != null && training.ClientId == client.Id)
            {
                training.ClientId = null;
                await _individualTrainingRepository.UpdateAsync(training);
                return Result.Success();
            }
            else
            {
                return Result.Error("Trening nie jest zarezerwowany");
            }
        }

        public async Task<Result> Reserve(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var membership = await _membershipService.GetActiveMembership(client.Id);

            if (membership == null)
            {
                return Result.Error("Nie znaleziono aktywnego karnetu dla danego klienta");
            }

            var membershipPlan = await _membershipPlanRepository.GetByIdAsync(membership.MembershipPlanId);

            if (membershipPlan == null || !membershipPlan.CanReserveTrainings)
            {
                return Result.Error("Nie znaleziono planu lub plan nie pozwala na rezerwację zajęć");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }
            
            if(training.ClientId != null)
            {
                var trainingClient = await _clientRepository.GetByIdAsync(training.ClientId);
                if (trainingClient != null)
                {
                    return Result.Error("Trening jest już zarezerwowany");
                }
                else
                {
                    training.ClientId = client.Id;
                    await _individualTrainingRepository.UpdateAsync(training);
                    return Result.Success();
                }
            }

            training.ClientId = client.Id;
            await _individualTrainingRepository.UpdateAsync(training);
            return Result.Success();
        }
    }
}
