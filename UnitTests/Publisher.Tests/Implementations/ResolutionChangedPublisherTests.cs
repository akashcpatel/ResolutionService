using Model;
using Moq;
using NUnit.Framework;
using Publisher.Implementations;
using System;
using System.Threading.Tasks;

namespace Publisher.Tests.Implementations
{
    [TestFixture]
    public class ResolutionChangedPublisherTests
    {
        private Mock<IAsyncCommunicator> _asyncCommunicatorMock;
        private ResolutionChangedPublisher _resolutionChangedPublisher;
        private PublisherConfig _publisherConfig;

        [SetUp]
        public void SetUp()
        {
            _publisherConfig = new PublisherConfig
            {
                UserChangedQueue = "userChangedQueue",
                ResolutionChangedQueue = "resolutionChangedQueue"
            };

            _asyncCommunicatorMock = new Mock<IAsyncCommunicator>();
            _resolutionChangedPublisher = new ResolutionChangedPublisher(_asyncCommunicatorMock.Object, _publisherConfig);
        }

        [Test]
        public async Task Add_Should_Send()
        {
            var resolution = CreateTestResolution();

            await _resolutionChangedPublisher.Add(resolution);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.ResolutionChangedQueue, It.Is<string>(x =>
                x.Contains("\"ChangeType\":0},\"Payload\":{\"Id\":\"05b9bc33-510e-4209-8e57-ef9d90027e78\",\"UserId\":\"bd147a27-2c37-4079-9b35-7950fbc65163\",\"Target\":\"Some target\",\"TargetDate\":\"2022-01-31T01:01:01\",\"Achieved\":true}}"))),
                Times.Once);
        }

        [Test]
        public async Task Update_Should_Send()
        {
            var resolution = CreateTestResolution();

            await _resolutionChangedPublisher.Update(resolution);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.ResolutionChangedQueue, It.Is<string>(x =>
                    x.Contains("\"ChangeType\":1},\"Payload\":{\"Id\":\"05b9bc33-510e-4209-8e57-ef9d90027e78\",\"UserId\":\"bd147a27-2c37-4079-9b35-7950fbc65163\",\"Target\":\"Some target\",\"TargetDate\":\"2022-01-31T01:01:01\",\"Achieved\":true}}"))),
                Times.Once);
        }

        [Test]
        public async Task Delete_Should_Send()
        {
            var resolution = CreateTestResolution();

            await _resolutionChangedPublisher.Delete(resolution.Id);

            _asyncCommunicatorMock.Verify(a =>
                a.Send(_publisherConfig.ResolutionChangedQueue, It.Is<string>(x => x.Contains("\"ChangeType\":2") && 
                x.Contains($"\"Id\":\"{resolution.Id}\""))),
                Times.Once);
        }

        private Resolution CreateTestResolution() =>
            new Resolution
            {
                Id = Guid.Parse("05b9bc33-510e-4209-8e57-ef9d90027e78"),
                Achieved = true,
                Target = "Some target",
                TargetDate = new DateTime(2022, 1, 31, 1, 1, 1),
                UserId = Guid.Parse("bd147a27-2c37-4079-9b35-7950fbc65163")
            };
    }
}