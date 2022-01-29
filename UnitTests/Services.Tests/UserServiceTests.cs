using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Publisher;
using Services.Implementations;
using Storage;

namespace Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private ResolutionService _userService;
        private Mock<ILogger<ResolutionService>> _loggerMock;
        private ServicesConfig _config;
        private Mock<IResolutionRepository> _userRepositoryMock;
        private Mock<IResolutionChangedPublisher> _userChangedPublisherMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ResolutionService>>();
            _config = new ServicesConfig();
            _userRepositoryMock = new Mock<IResolutionRepository>();
            _userChangedPublisherMock = new Mock<IResolutionChangedPublisher>();

            _userService = new ResolutionService(_loggerMock.Object, _config,
                _userChangedPublisherMock.Object, _userRepositoryMock.Object);
        }

        [Test]
        public async Task UpSert_Should_Call_Add()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Find(testUser.Id)).Returns(() => null);

            await TestUpsert(0, 1, testUser);
        }

        [Test]
        public async Task UpSert_Should_Call_Update()
        {
            var testUser = CreateTestUser();
            _userRepositoryMock.Setup(r => r.Find(testUser.Id)).ReturnsAsync(() => testUser);

            await TestUpsert(1, 0, testUser);
        }

        [Test]
        public async Task Delete_By_Id_Should_Call_Delete()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Delete(testUser.Id));

            await _userService.Delete(testUser.Id);

            _userRepositoryMock.Verify(r => r.Delete(testUser.Id), Times.Once);
        }

        [Test]
        public async Task Delete_By_Username_Should_Call_Delete()
        {
            var testUser = CreateTestUser();

            _userRepositoryMock.Setup(r => r.Delete(testUser.Id));

            await _userService.Delete(testUser.Id);

            _userRepositoryMock.Verify(r => r.Delete(testUser.Id), Times.Once);
        }

        private async Task TestUpsert(int updateCount, int addCount, Resolution testUser)
        {
            _userRepositoryMock.Setup(r => r.Update(testUser)).ReturnsAsync(testUser.Id);
            _userRepositoryMock.Setup(r => r.Add(testUser)).ReturnsAsync(testUser.Id);

            await _userService.UpSert(testUser);

            _userRepositoryMock.Verify(r => r.Find(testUser.Id), Times.Once);
            _userRepositoryMock.Verify(r => r.Add(testUser), Times.Exactly(addCount));
            _userRepositoryMock.Verify(r => r.Update(testUser), Times.Exactly(updateCount));
        }

        private Resolution CreateTestUser() =>
            new Resolution
            {
                //FirstName = "First Name",
                //LastName = " Last Name",
                //Id = Guid.NewGuid(),
                //UserName = "username"
            };
    }
}