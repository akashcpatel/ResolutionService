using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Storage.Implementations;
using System.Threading.Tasks;

namespace Storage.Tests.Implementations
{
    [TestFixture]
    public class ResolutionRepositoryTests
    {
        private ResolutionRepository _resolutionRepository;
        private readonly Mock<ResolutionDataContext> _contextMock = new Mock<ResolutionDataContext>();

        [SetUp]
        public void SetUp()
        {
            var logger = new Mock<ILogger<ResolutionRepository>>();

            _resolutionRepository = new ResolutionRepository(_contextMock.Object, logger.Object);
        }

        [Test]
        public async Task Test_Abc()
        {
            await _resolutionRepository.Save(new Model.Resolution());
        }
    }
}
