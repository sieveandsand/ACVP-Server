﻿using NIST.CVP.ACVTS.Libraries.Generation.KDF.v1_0;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.KDF
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
        [TestCase("keyOut")]
        [TestCase("KEYOUT")]
        [TestCase("ko")]
        [TestCase("KO")]
        public void ShouldSetKeyOut(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsTrue(result);
            Assert.AreEqual("00AA", subject.KeyOut.ToHex());
        }

        [Test]
        [TestCase("KEYIN")]
        [TestCase("KeyIn")]
        [TestCase("ki")]
        [TestCase("kI")]
        public void ShouldSetKeyIn(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsTrue(result);
            Assert.AreEqual("00AA", subject.KeyIn.ToHex());
        }
    }
}
