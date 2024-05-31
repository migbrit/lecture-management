using LectureManagement.Entities;
using LectureManagement.Factories.Interfaces;
using LectureManagement.Services.Interfaces;
using Moq;

namespace LectureManagement.Tests.Unit
{
    public class LectureManagementTests
    {
        private Mock<ILectureFileService> mockLectureFileService;
        private Mock<ITrackFactory> mockTrackFactory;
        private LectureManagerService service;

        public LectureManagementTests()
        {
            mockLectureFileService = new Mock<ILectureFileService>();
            mockTrackFactory = new Mock<ITrackFactory>();
            service = new LectureManagerService(mockLectureFileService.Object, mockTrackFactory.Object);
        }

        [Fact]
        public async Task ReadLecturesFromFileAsync_ShouldReturnLectures()
        {
            // Arrange
            var lectures = new List<Lecture>
            {
                new Lecture { Name = "Test Lecture", Duration = "60min"}
            };

            mockLectureFileService.Setup(s => s.ReadLecturesFromFileAsync(It.IsAny<string>()))
                .ReturnsAsync(lectures);

            // Act
            var result = await service.ReadLecturesFromFileAsync("test.txt");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Lecture", result[0].Name);
            Assert.Equal("60min", result[0].Duration);
        }

        [Fact]
        public async Task ReadLecturesFromFileAsync_ShouldThrowNotFoundExceptionWhenFileDoesNotExist()
        {
            // Arrange
            mockLectureFileService.Setup(s => s.ReadLecturesFromFileAsync(It.IsAny<string>()))
                .ThrowsAsync(new FileNotFoundException());

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => service.ReadLecturesFromFileAsync("notexists.txt"));
        }

        [Fact]
        public async Task ReadLecturesFromFileAsync_ShouldReturnEmptyList_WhenFileIsEmpty()
        {
            // Arrange
            mockLectureFileService.Setup(s => s.ReadLecturesFromFileAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Lecture>());

            // Act
            var result = await service.ReadLecturesFromFileAsync("empty.txt");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ReadLecturesFromFileAsync_ShouldThrowFormatExceptionForInvalidDuration()
        {
            // Arrange
            mockLectureFileService.Setup(s => s.ReadLecturesFromFileAsync(It.IsAny<string>()))
                .ThrowsAsync(new FormatException("Invalid lecture duration format."));

            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => service.ReadLecturesFromFileAsync("invalid.txt"));
        }


    }
}
