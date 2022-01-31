using NUnit.Framework;
using System;

namespace Model.Tests
{
    public class ResolutionTests
    {
        [Test]
        public void ConstructionTests()
        {
            var resolution = new Resolution();
            Assert.That(resolution.Id, Is.EqualTo(Guid.Empty));
            Assert.That(resolution.Target, Is.Null);
            Assert.That(resolution.TargetDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(resolution.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(resolution.Achieved, Is.False);
        }
    }
}