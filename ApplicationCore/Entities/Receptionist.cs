using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Receptionist : Employee
    {
        public List<Availability<Receptionist>> Availabilities { get; set; } = new List<Availability<Receptionist>>();
        public List<Shift<Receptionist>> Shifts { get; set; } = new List<Shift<Receptionist>>();
    }
}

