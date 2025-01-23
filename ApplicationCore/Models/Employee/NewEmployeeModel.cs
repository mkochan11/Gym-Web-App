using ApplicationCore.Enums;

namespace ApplicationCore.Models.User
{
    public class NewEmployeeModel
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? EmploymentDate { get; set; }
    }
}