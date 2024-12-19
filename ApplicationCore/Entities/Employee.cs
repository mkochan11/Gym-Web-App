using ApplicationCore.Entities.Abstract;
using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Employee : User
    {
        public Position Position { get; set; }
        public DateTime EmploymentDate { get; set; }
        public decimal Salary { get; set; }
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
