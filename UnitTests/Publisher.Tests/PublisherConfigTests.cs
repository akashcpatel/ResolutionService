using NUnit.Framework;

namespace Publisher.Tests
{
    public class PublisherConfigTests
    {
        [Test]
        public void PositionInConfig_Should_Be_Set()
        {
            Assert.That(PublisherConfig.PositionInConfig, Is.EqualTo("Publisher"));
        }
    }
}