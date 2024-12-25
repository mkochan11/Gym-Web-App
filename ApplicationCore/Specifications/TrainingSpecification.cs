using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class FindIndividualTrainigByMonth : SingleResultSpecification<IndividualTraining>
    {
        public FindIndividualTrainigByMonth(int month, int year)
        {
            Query.Where(x => x.Date.Month == month && x.Date.Year == year).AsSplitQuery();
        }
    }

    public class FindGroupTrainigByMonth : SingleResultSpecification<GroupTraining>
    {
        public FindGroupTrainigByMonth(int month, int year)
        {
            Query.Where(x => x.Date.Month == month && x.Date.Year == year).AsSplitQuery();
        }
    }
}
