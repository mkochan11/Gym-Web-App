namespace ApplicationCore.Models.Exercise
{
    public class NewExerciseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RepetitionsNumber { get; set; }
        public int SeriesNumber { get; set; }
        public TimeSpan RestTime { get; set; }
    }
}
