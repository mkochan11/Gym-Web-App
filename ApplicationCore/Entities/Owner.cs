using ApplicationCore.Entities.Abstract;

namespace ApplicationCore.Entities
{
    public class Owner : Employee
    {
        public List<GymReport> GymReports { get; set; }
    }
}
