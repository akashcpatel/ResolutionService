using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Publisher;
using Services.Implementations;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestFixture]
    public class ResolutionServiceTests
    {
        private ResolutionService _resolutionService;
        private Mock<ILogger<ResolutionService>> _loggerMock;
        private ServicesConfig _config;
        private Mock<IResolutionRepository> _resolutionRepositoryMock;
        private Mock<IResolutionChangedPublisher> _resolutionChangedPublisherMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ResolutionService>>();
            _config = new ServicesConfig();
            _resolutionRepositoryMock = new Mock<IResolutionRepository>();
            _resolutionChangedPublisherMock = new Mock<IResolutionChangedPublisher>();

            _resolutionService = new ResolutionService(_loggerMock.Object, _config,
                _resolutionChangedPublisherMock.Object, _resolutionRepositoryMock.Object);
        }

        [Test]
        public async Task UpSert_Should_Call_Add()
        {
            var testResolution = CreateResolution();

            _resolutionRepositoryMock.Setup(r => r.Find(testResolution.Id)).ReturnsAsync(() => null);

            await TestUpsert(1, testResolution);
            _resolutionChangedPublisherMock.Verify(p => p.Add(testResolution), Times.Once);
        }

        [Test]
        public async Task UpSert_Should_Call_Update()
        {
            var testResolution = CreateResolution();
            _resolutionRepositoryMock.Setup(r => r.Find(testResolution.Id)).ReturnsAsync(() => testResolution);

            await TestUpsert(1, testResolution);
            _resolutionChangedPublisherMock.Verify(p => p.Update(testResolution), Times.Once);
        }

        [Test]
        public async Task Delete_By_Id_Should_Call_Delete()
        {
            var testResolution = CreateResolution();

            _resolutionRepositoryMock.Setup(r => r.Delete(testResolution.Id));

            await _resolutionService.Delete(testResolution.Id);

            _resolutionRepositoryMock.Verify(r => r.Delete(testResolution.Id), Times.Once);

            _resolutionChangedPublisherMock.Verify(p => p.Delete(testResolution.Id), Times.Once);
        }

        private async Task TestUpsert(int saveCount, Resolution testResolution)
        {
            _resolutionRepositoryMock.Setup(r => r.Save(testResolution)).ReturnsAsync(testResolution.Id);

            await _resolutionService.UpSert(testResolution);

            _resolutionRepositoryMock.Verify(r => r.Find(testResolution.Id), Times.Once);
            _resolutionRepositoryMock.Verify(r => r.Save(testResolution), Times.Exactly(saveCount));
        }

        private Resolution CreateResolution() =>
            new Resolution
            {
                Id = Guid.NewGuid(),
                Target = "My target",
                TargetDate = DateTime.Now.AddDays(30),
                UserId = Guid.NewGuid(),
                Achieved = true
            };
    }
}