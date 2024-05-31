using LectureManagement;
using LectureManagement.Factories;
using LectureManagement.Factories.Interfaces;
using LectureManagement.Services;
using LectureManagement.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

try
{
    var serviceProvider = ConfigureServices();
    var lectureManagerService = serviceProvider.GetRequiredService<ILectureManagerService>();

    string? projectBasePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
    if (projectBasePath == null)
        throw new DirectoryNotFoundException("Project base path not found.");

    string filePath = Path.Combine(projectBasePath, "Data", "lectures.txt");
    if (!File.Exists(filePath))
        throw new FileNotFoundException($"File {filePath} not found in the system.");

    var lectures = await lectureManagerService.ReadLecturesFromFileAsync(filePath);
    var tracks = lectureManagerService.ScheduleLectures(lectures);
    lectureManagerService.PrintSchedule(tracks);
    Console.ReadLine();
}
catch(Exception e)
{
    Console.WriteLine("An error has occurred: " + e);
}

static ServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    // Register services and factories
    services.AddSingleton<ILectureManagerService, LectureManagerService>();
    services.AddSingleton<ILectureFileService, LectureFileService>();
    services.AddSingleton<ITrackFactory, TrackFactory>();

    return services.BuildServiceProvider();
}

