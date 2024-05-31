namespace LectureManagement.Entities
{
    public class Track
    {
        public List<Lecture> MorningSession { get; set; }
        public List<Lecture> AfternoonSession { get; set; }
    }
}
