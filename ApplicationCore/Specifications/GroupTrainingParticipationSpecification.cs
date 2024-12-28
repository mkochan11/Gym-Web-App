using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindParticipationByClientId : SingleResultSpecification<GroupTrainingParticipation>
    {
        public FindParticipationByClientId(int clientId)
        {
            Query.Where(x => x.ClientId.ToString() == clientId.ToString()).AsSplitQuery();
        }
    }
    public class FindParticipationByTrainingId : SingleResultSpecification<GroupTrainingParticipation>
    {
        public FindParticipationByTrainingId(int trainingId)
        {
            Query.Where(x => x.GroupTrainingId.ToString() == trainingId.ToString()).AsSplitQuery();
        }
    }
}