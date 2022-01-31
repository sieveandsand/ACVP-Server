﻿using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Generation.TDES_CTR.v1_0;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.TDES.CTR
{
    [TestFixture, UnitTest]
    public class TestCaseGeneratorNullTests
    {
        [Test]
        public void ShouldHaveOneForNumberOfTestCases()
        {
            var subject = new TestCaseGeneratorNull();
            Assert.AreEqual(1, subject.NumberOfTestCasesToGenerate);
        }

        [Test]
        public async Task ShouldReturnErrorForInitialGenerate()
        {
            var subject = new TestCaseGeneratorNull();
            var result = await subject.GenerateAsync(new TestGroup(), false);
            Assert.IsFalse(result.Success);
        }
    }
}
