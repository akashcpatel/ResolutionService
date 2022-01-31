using NUnit.Framework;
using Publisher.Message;
using System;

namespace Publisher.Tests.Message
{
    public class HeaderTests
    {
        [Test]
        public void ConstructionTests()
        {
            var header = new Header();
            Assert.That(header.Key, Is.EqualTo(Guid.Empty));
            Assert.That(header.ChangeType, Is.EqualTo(ChangeType.Add));
        }
    }
}
