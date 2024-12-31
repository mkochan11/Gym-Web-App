using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindTrainingPlanByTrainerId : SingleResultSpecification<TrainingPlan>
    {
        public FindTrainingPlanByTrainerId(int trainerId)
        {
            Query.Where(x => x.PersonalTrainerId == trainerId).AsSplitQuery();
        }
    }
}
