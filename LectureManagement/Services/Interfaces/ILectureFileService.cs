using LectureManagement.Entities;

namespace LectureManagement.Services.Interfaces
{
    public interface ILectureFileService
    {
        Task<List<Lecture>> ReadLecturesFromFileAsync(string filePath);
    }
}
