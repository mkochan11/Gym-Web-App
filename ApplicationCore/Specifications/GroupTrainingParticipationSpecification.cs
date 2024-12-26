using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
