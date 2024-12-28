using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
