using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Abstract
{
    public abstract class Employee : User
    {
        public Position Position { get; set; }
        public DateTime EmploymentDate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Salary { get; set; }
    }
}
