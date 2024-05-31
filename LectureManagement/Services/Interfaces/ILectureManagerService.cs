using LectureManagement.Entities;

namespace LectureManagement.Services.Interfaces
{
    public interface ILectureManagerService
    {
        Task<List<Lecture>> ReadLecturesFromFileAsync(string filePath);
        List<Track> ScheduleLectures(List<Lecture> lectures);
        void PrintSchedule(List<Track> tracks);
    }
}
