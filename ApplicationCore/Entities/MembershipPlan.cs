using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MembershipPlan : BaseEntity
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public string DurationTime { get; set; } = string.Empty;
        public int DurationInMonths { get; set; }
        public bool CanReserveTrainings { get; set; }
    }
}
