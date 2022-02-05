using NUnit.Framework;
using System;

namespace Model.Tests
{
    public class UserTests
    {
        [Test]
        public void ConstructionTests()
        {
            var user = new User();
            Assert.That(user.Id, Is.EqualTo(Guid.Empty));
            Assert.That(user.FirstName, Is.Null);
            Assert.That(user.LastName, Is.Null);
        }
    }
}