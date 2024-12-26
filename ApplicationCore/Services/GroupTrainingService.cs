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
    public class GroupTrainingService : IGroupTrainingService
    {
        IRepository<Client> _clientRepository;
        IRepository<GymMembership> _gymMembershipRepository;
        IRepository<MembershipPlan> _membershipPlanRepository;
        IRepository<GroupTraining> _groupTrainingRepository;
        IRepository<GroupTrainingParticipation> _groupTrainingParticipationRepository;
        IMembershipService _membershipService;

        public GroupTrainingService(
            IRepository<Client> clientRepository,
            IRepository<GymMembership>  gymMemberhsipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<GroupTraining> groupTrainingRepository,
            IRepository<GroupTrainingParticipation> groupTrainingParticipationRepository,
            IMembershipService membershipService
            )
        {
            _clientRepository = clientRepository;
            _gymMembershipRepository = gymMemberhsipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _groupTrainingRepository = groupTrainingRepository;
            _groupTrainingParticipationRepository = groupTrainingParticipationRepository;
            _membershipService = membershipService;
        }

        public async Task<Result> CancelPlace(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var training = await _groupTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            var _participationSpec = new FindParticipationByTrainingId(training.Id);
            var participations = await _groupTrainingParticipationRepository.ListAsync(_participationSpec);

            GroupTrainingParticipation participation = null;

            foreach (var part in participations)
            {
                if(part.ClientId == client.Id) participation = part;
            }

            if (participation == null)
            {
                return Result.Error("Nie znaleziono rezerwacji miejsca");
            }

            await _groupTrainingParticipationRepository.DeleteAsync(participation);

            return Result.Success();

        }

        public async Task<Result> ReservePlace(int trainingId, string userId)
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

            var training = await _groupTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            var _participationSpec = new FindParticipationByTrainingId(training.Id);
            var participations = await _groupTrainingParticipationRepository.ListAsync(_participationSpec);

            training.Participations = participations;

            if (training.Participations.Count >= training.MaxParticipantNumber)
            {
                return Result.Error("Trening jest już pełny");
            }

            if (training.Participations.Any(p => p.ClientId == client.Id))
            {
                return Result.Error("Klient jest już zapisany na ten trening");
            }

            var groupTrainingParticipation = new GroupTrainingParticipation
            {
                ClientId = client.Id,
                GroupTrainingId = training.Id,
            };

            await _groupTrainingParticipationRepository.AddAsync(groupTrainingParticipation);

            return Result.Success();
            
        }
    }
}
