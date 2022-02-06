using NUnit.Framework;
using System;

namespace Application.Tests
{
    [TestFixture]
    public class GuardTests
    {
        [TestCase("0f8fad5b-d9cb-469f-a165-70867728950e", null)]
        [TestCase(null, "id is Null or Empty")]
        [TestCase("00000000-0000-0000-0000-000000000000", "id is Null or Empty")]
        public void Id(Guid id, string expectedMessage)
        {
            string exceptionMessage = null;
            try
            {
                Guard.Id(id);
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                exceptionMessage = ex.Message;
            }

            Assert.That(exceptionMessage, Is.EqualTo(expectedMessage));
        }

        //[TestCase("first Name", null)]
        //[TestCase(null, "firstName is Null or Empty")]
        //[TestCase("", "firstName is Null or Empty")]
        //[TestCase("  ", "firstName is Null or Empty")]
        //public void FirstName(string firstName, string expectedMessage)
        //{
        //    string exceptionMessage = null;
        //    try
        //    {
        //        Guard.FirstName(firstName);
        //    }
        //    catch (Exception ex) when (ex is ArgumentException)
        //    {
        //        exceptionMessage = ex.Message;
        //    }

        //    Assert.That(exceptionMessage, Is.EqualTo(expectedMessage));
        //}

        //[TestCase("last Name", null)]
        //[TestCase(null, "lastName is Null or Empty")]
        //[TestCase("", "lastName is Null or Empty")]
        //[TestCase("  ", "lastName is Null or Empty")]
        //public void LastName(string lastName, string expectedMessage)
        //{
        //    string exceptionMessage = null;
        //    try
        //    {
        //        Guard.LastName(lastName);
        //    }
        //    catch (Exception ex) when (ex is ArgumentException)
        //    {
        //        exceptionMessage = ex.Message;
        //    }

        //    Assert.That(exceptionMessage, Is.EqualTo(expectedMessage));
        //}

        //[TestCase("user Name", null)]
        //[TestCase(null, "userName is Null or Empty")]
        //[TestCase("", "userName is Null or Empty")]
        //[TestCase("  ", "userName is Null or Empty")]
        //public void UserName(string userName, string expectedMessage)
        //{
        //    string exceptionMessage = null;
        //    try
        //    {
        //        Guard.Target(userName);
        //    }
        //    catch (Exception ex) when (ex is ArgumentException)
        //    {
        //        exceptionMessage = ex.Message;
        //    }

        //    Assert.That(exceptionMessage, Is.EqualTo(expectedMessage));
        //}
    }
}
