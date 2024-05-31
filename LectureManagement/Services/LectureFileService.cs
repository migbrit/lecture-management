using LectureManagement.Entities;
using LectureManagement.Services.Interfaces;

namespace LectureManagement.Services
{
    public class LectureFileService : ILectureFileService
    {
        public async Task<List<Lecture>> ReadLecturesFromFileAsync(string filePath)
        {
            var lectures = new List<Lecture>();
            var lines = await File.ReadAllLinesAsync(filePath);
            foreach (var line in lines)
                lectures.Add(GetLectureFromLine(line));

            return lectures;
        }

        private Lecture GetLectureFromLine(string line)
        {
            int lastSpaceIndex = line.LastIndexOf(' ');
            if (lastSpaceIndex == -1 || lastSpaceIndex == line.Length - 1)
                throw new FormatException("Invalid data. The duration of the lecture was not found.");

            var title = line.Substring(0, lastSpaceIndex);
            var duration = line.Substring(lastSpaceIndex + 1);

            if (duration.EndsWith("min"))
                duration = duration.Replace("min", "");
            else
                if (duration != "relâmpago")
                    throw new FormatException("Invalid lecture duration format.");

            return new Lecture { Name = title, Duration = duration };
        }

    }
}
