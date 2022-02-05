using Main.Controllers;
using Main.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Moq;
using NUnit.Framework;
using Services;
using System;
using System.Threading.Tasks;

namespace Main.Tests.Controllers
{
    [TestFixture]
    public class ResolutionControllerTests
    {
        private Mock<IResolutionService> _resolutionServiceMock;
        private ResolutionController _controller;

        [SetUp]
        public void SetUp()
        {
            var _loggerMock = new Mock<ILogger<ResolutionController>>();
            _resolutionServiceMock = new Mock<IResolutionService>();

            _controller = new ResolutionController(_loggerMock.Object, _resolutionServiceMock.Object);
        }

        [Test]
        public async Task Save_Should_Throw_ValidationError()
        {
            var dto = new ResolutionDto();
            string exceptionMessage = "";
            try
            {
                await _controller.Save(dto);
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionMessage, Is.EqualTo("id is Null or Empty"));

            _resolutionServiceMock.Verify(r => r.UpSert(It.IsAny<Resolution>()), Times.Never);
        }

        [Test]
        public async Task Save_Should_UpSert()
        {
            var dto = CreateTestResolution();

            ActionResult<Guid> upsertResponse = null;

            _resolutionServiceMock.Setup(
                r =>
                r.UpSert(It.Is<Resolution>(r =>
                                          r.Target == dto.Target && r.TargetDate == dto.TargetDate &&
                                          r.Achieved == dto.Achieved && r.UserId == dto.UserId &&
                                          r.Id == dto.Id))).ReturnsAsync(dto.Id);

            upsertResponse = await _controller.Save(dto);

            _resolutionServiceMock.Verify(r => r.UpSert(It.Is<Resolution>(r =>
                                          r.Target == dto.Target && r.TargetDate == dto.TargetDate &&
                                          r.Achieved == dto.Achieved && r.UserId == dto.UserId &&
                                          r.Id == dto.Id)), Times.Once);

            Assert.That(((ObjectResult)upsertResponse.Result).Value, Is.EqualTo(dto.Id));
        }

        [Test]
        public async Task Get_By_Id_Should_Find()
        {
            var dto = CreateTestResolution();

            var resolution = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _resolutionServiceMock.Setup(r => r.Find(dto.Id)).ReturnsAsync(resolution);

            findResponse = await _controller.Get(dto.Id);
            var foundUser = (Resolution)((ObjectResult)findResponse.Result).Value;

            Assert.True(foundUser.Equals(resolution));
        }

        [Test]
        public async Task Delete_By_Id_Should_Throw_ValidationError()
        {
            string exceptionMessage = "";
            try
            {
                await _controller.Delete(Guid.Empty);
            }
            catch (ArgumentException ex)
            {
                exceptionMessage = ex.Message;
            }

            _resolutionServiceMock.Verify(r => r.Find(It.IsAny<Guid>()), Times.Never());
            Assert.That(exceptionMessage, Is.EqualTo("id is Null or Empty"));
        }

        [Test]
        public async Task Delete_By_Id_When_Delete_Pass()
        {
            var dto = CreateTestResolution();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _resolutionServiceMock.Setup(r => r.Delete(dto.Id)).ReturnsAsync(true);

            findResponse = await _controller.Delete(dto.Id);
            var deleteResult = ((StatusCodeResult)findResponse.Result).StatusCode;

            Assert.That(deleteResult, Is.EqualTo(200));
        }

        [Test]
        public async Task Delete_By_Id_When_Delete_Fail()
        {
            var dto = CreateTestResolution();

            var user = dto.ToModel();

            ActionResult<Guid> findResponse = null;

            _resolutionServiceMock.Setup(r => r.Delete(dto.Id)).ReturnsAsync(false);

            findResponse = await _controller.Delete(dto.Id);
            var deleteResultMessage = ((ObjectResult)findResponse.Result).Value.ToString();

            Assert.That(((ObjectResult)findResponse.Result).StatusCode, Is.EqualTo(417));
            Assert.True(deleteResultMessage.Contains("Failed to delete Resolution for Id"));
        }

        private ResolutionDto CreateTestResolution() =>
            new ResolutionDto
            {
                Id = Guid.NewGuid(),
                Achieved = true,
                Target = "Some target",
                TargetDate = DateTime.Now,
                UserId = Guid.NewGuid()
            };
    }
}
