namespace ApplicationCore.Entities.Abstract
{
    public abstract class Trainer<T> : Employee
    {
        public List<T> Trainings { get; set; } = new List<T>();
    }
}
