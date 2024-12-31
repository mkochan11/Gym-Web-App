using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindExerciseByTrainingPlanId : SingleResultSpecification<Exercise>
    {
        public FindExerciseByTrainingPlanId(int planId)
        {
            Query.Where(x => x.TrainingPlanId == planId).AsSplitQuery();
        }
    }
}
