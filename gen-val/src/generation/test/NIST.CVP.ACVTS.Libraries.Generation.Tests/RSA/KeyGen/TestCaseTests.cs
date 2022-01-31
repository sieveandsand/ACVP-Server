﻿using NIST.CVP.ACVTS.Libraries.Generation.RSA.v1_0.KeyGen;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.RSA.KeyGen
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
        [TestCase("seed")]
        [TestCase("SeEd")]
        public void ShouldSetSeed(string name)
        {
            var subject = new TestCase();
            var result = subject.SetString(name, "00AA");
            Assert.IsTrue(result);
            Assert.AreEqual(new BitString("00AA"), subject.Seed);
        }
    }
}
