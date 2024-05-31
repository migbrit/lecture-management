using LectureManagement.Entities;
using LectureManagement.Factories.Interfaces;

namespace LectureManagement.Factories
{
    public class TrackFactory : ITrackFactory
    {
        public Track CreateTrack()
        {
            return new Track()
            { 
                MorningSession = new List<Lecture>(),
                AfternoonSession = new List<Lecture>()
            };
        }
    }
}
