using LectureManagement.Entities;
using LectureManagement.Factories.Interfaces;
using LectureManagement.Services.Interfaces;

namespace LectureManagement
{
    public class LectureManagerService : ILectureManagerService
    {
        private const int MorningSessionDuration = 180; // 3 horas em minutos
        private const int AfternoonSessionDuration = 240; // 4 horas em minutos
        private const int NetworkingEventMinStartTime = 16 * 60; // 16:00H em minutos
        private const int NetworkingEventMaxStartTime = 17 * 60; // 17:00H em minutos
        private const int LightningTalkDuration = 5; // Duração de uma palestra relâmpago

        private readonly ILectureFileService _lectureFileService;
        private readonly ITrackFactory _trackFactory;

        public LectureManagerService(ILectureFileService lectureFileService, ITrackFactory trackFactory)
        {
            _lectureFileService = lectureFileService;
            _trackFactory = trackFactory;
        }

        public async Task<List<Lecture>> ReadLecturesFromFileAsync(string filePath)
        {
            return await _lectureFileService.ReadLecturesFromFileAsync(filePath);
        }

        public List<Track> ScheduleLectures(List<Lecture> lectures)
        {
            var tracks = new List<Track>();
            var currentTrack = _trackFactory.CreateTrack();

            int morningTimeLeft = MorningSessionDuration;
            int afternoonTimeLeft = AfternoonSessionDuration;

            foreach (var lecture in lectures)
                ExecuteScheduling(tracks, ref currentTrack, ref morningTimeLeft, ref afternoonTimeLeft, lecture);

            tracks.Add(currentTrack);
            return tracks;
        }

        private void ExecuteScheduling(List<Track> tracks, ref Track currentTrack, ref int morningTimeLeft, ref int afternoonTimeLeft, Lecture lecture)
        {
            int duration = GetLectureDuration(lecture);

            if (morningTimeLeft >= duration)
            {
                currentTrack.MorningSession.Add(lecture);
                morningTimeLeft -= duration;
            }
            else if (afternoonTimeLeft >= duration)
            {
                currentTrack.AfternoonSession.Add(lecture);
                afternoonTimeLeft -= duration;
            }
            else
            {
                tracks.Add(currentTrack);
                currentTrack = _trackFactory.CreateTrack();

                morningTimeLeft = MorningSessionDuration;
                afternoonTimeLeft = AfternoonSessionDuration;

                if (morningTimeLeft >= duration)
                {
                    currentTrack.MorningSession.Add(lecture);
                    morningTimeLeft -= duration;
                }
                else
                {
                    currentTrack.AfternoonSession.Add(lecture);
                    afternoonTimeLeft -= duration;
                }
            }
        }

        public void PrintSchedule(List<Track> tracks)
        {
            int trackNumber = 1;
            foreach (var track in tracks)
            {
                Console.WriteLine($"Trilha {trackNumber}:");

                PrintSession(track.MorningSession, 9 * 60, "Almoço", 12 * 60);
                PrintSession(track.AfternoonSession, 13 * 60, "Networking Event", 16 * 60, true);

                trackNumber++;
            }
        }

        private int GetLectureDuration(Lecture lecture)
        {
            return lecture.Duration == "relâmpago" ? LightningTalkDuration : int.Parse(lecture.Duration);
        }

        private void PrintSession(List<Lecture> session, int startTime, string breakMessage, int breakTime, bool isNetworkingEvent = false)
        {
            foreach (var lecture in session)
            {
                PrintLecture(lecture, startTime);
                startTime += GetLectureDuration(lecture);
            }

            if (!isNetworkingEvent)
                Console.WriteLine($"{TimeToString(breakTime)}H {breakMessage}");
            else
            {
                int networkingEventStartTime = Math.Max(startTime, NetworkingEventMinStartTime);
                networkingEventStartTime = Math.Min(networkingEventStartTime, NetworkingEventMaxStartTime);
                Console.WriteLine($"{TimeToString(networkingEventStartTime)}H {breakMessage}\n");
            }
        }

        private void PrintLecture(Lecture lecture, int startTime)
        {
            var timeString = TimeToString(startTime);
            var durationString = lecture.Duration == "relâmpago" ? "relâmpago" : $"{lecture.Duration}min";
            Console.WriteLine($"{timeString}H {lecture.Name} {durationString}");
        }

        private string TimeToString(int timeInMinutes)
        {
            var hours = timeInMinutes / 60;
            var minutes = timeInMinutes % 60;
            return $"{hours:D2}:{minutes:D2}";
        }
    }
}
