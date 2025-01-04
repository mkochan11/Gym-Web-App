
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class GymReport : BaseEntity
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? NewClients { get; set; }
        public int? NewMemberships { get; set; }
        public decimal? TotalIncome { get; set; }
        public int? TotalIndividualTrainings { get; set; }
        public int? TotalGroupTrainings { get; set; }
        public TimeSpan? TotalIndividualTrainingsTime { get; set; }
        public TimeSpan? TotalGroupTrainingsTime { get; set; }
        public decimal? TotalEmployeesCost { get; set; }
    }
}
