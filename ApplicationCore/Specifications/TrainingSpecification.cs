using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindIndividualTrainingByMonth : SingleResultSpecification<IndividualTraining>
    {
        public FindIndividualTrainingByMonth(int month, int year)
        {
            Query.Where(x => x.Date.Month == month && x.Date.Year == year).AsSplitQuery();
        }
    }

    public class FindGroupTrainingByMonth : SingleResultSpecification<GroupTraining>
    {
        public FindGroupTrainingByMonth(int month, int year)
        {
            Query.Where(x => x.Date.Month == month && x.Date.Year == year).AsSplitQuery();
        }
    }

    public class FindTrainingsInPeriod<T> : SingleResultSpecification<T> where T : Training
    {
        public FindTrainingsInPeriod(DateTime from, DateTime to)
        {
            Query.Where(x => x.Date >= from && x.Date <= to).AsSplitQuery();
        }
}
}
