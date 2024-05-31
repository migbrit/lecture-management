using LectureManagement.Factories.Interfaces;
using LectureManagement.Services;
using Moq;

namespace LectureManagement.Tests.Integration
{
    public class LectureManagementIntegrationTests
    {
        private Mock<ITrackFactory> trackFactory;
        private LectureFileService lectureFileService;
        private LectureManagerService lectureManagerService;

        public LectureManagementIntegrationTests()
        {
            trackFactory = new Mock<ITrackFactory>();
            lectureFileService = new LectureFileService();
            lectureManagerService = new LectureManagerService(lectureFileService, trackFactory.Object);
        }

        [Fact]
        public async Task ReadLecturesFromFileAsync_ShouldThrowFormatExceptionForInvalidDuration()
        {
            // Arrange
            var filePath = "invalid.txt";
            await File.WriteAllTextAsync(filePath, "How to create microservices using C# rellamppagoo");

            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => lectureManagerService.ReadLecturesFromFileAsync(filePath));
        }
    }
}
