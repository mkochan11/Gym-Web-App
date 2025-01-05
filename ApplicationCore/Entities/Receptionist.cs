using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class Receptionist : Employee
    {
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}

