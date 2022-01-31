﻿using NIST.CVP.ACVTS.Libraries.Generation.KDF_Components.v1_0.TLS.v1_0;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.TLS
{
    [TestFixture, UnitTest]
    public class TestCaseTests
    {
        [Test]
        [TestCase("Fredo")]
        [TestCase("")]
        [TestCase("NULL")]
        [TestCase(null)]
        public void ShouldReturnFalseIfUnknownSetStringName(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("client_random")]
        [TestCase("clientRandom")]
        [TestCase("CLIENT_RANDOM")]
        public void ShouldSetClientRandom(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsTrue(result);
            Assert.AreEqual("00AA", subject.ClientRandom.ToHex());
        }

        [Test]
        [TestCase("server_random")]
        [TestCase("serverRandom")]
        [TestCase("SERVER_RANDOM")]
        public void ShouldSetServerRandom(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsTrue(result);
            Assert.AreEqual("00AA", subject.ServerRandom.ToHex());
        }
    }
}
