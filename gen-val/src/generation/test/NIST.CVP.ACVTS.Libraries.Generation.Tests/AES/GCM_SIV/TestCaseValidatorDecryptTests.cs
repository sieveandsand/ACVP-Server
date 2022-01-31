﻿using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Generation.AES_GCM_SIV.v1_0;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Enums;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.AES.GCM_SIV
{
    [TestFixture, UnitTest]
    public class TestCaseValidatorDecryptTests
    {
        [Test]
        public async Task ShouldValidateIfExpectedAndSuppliedResultsMatch()
        {
            var testCase = GetTestCase();
            var subject = new TestCaseValidatorDecrypt(testCase);
            var result = await subject.ValidateAsync(testCase);
            Assert.That(result != null);
            Assert.AreEqual(Disposition.Passed, result.Result);
        }

        [Test]
        public async Task ShouldFailIfPlainTextDoesNotMatch()
        {
            var testCase = GetTestCase();
            var subject = new TestCaseValidatorDecrypt(testCase);
            var suppliedResult = GetTestCase();
            suppliedResult.Plaintext = new BitString("D00000");
            var result = await subject.ValidateAsync(suppliedResult);
            Assert.That(result != null);
            Assert.AreEqual(Disposition.Failed, result.Result);
        }

        [Test]
        public async Task ShouldShowPlainTextAsReasonIfItDoesNotMatch()
        {
            var testCase = GetTestCase();
            var subject = new TestCaseValidatorDecrypt(testCase);
            var suppliedResult = GetTestCase();
            suppliedResult.Plaintext = new BitString("D00000");
            var result = await subject.ValidateAsync(suppliedResult);
            Assert.That(result != null);
            Assert.That(Disposition.Failed == result.Result);
            Assert.IsTrue(result.Reason.Contains("Plain Text"));
        }

        [Test]
        public async Task ShouldFailIfFailedTestDoesNotMatch()
        {
            var testCase = GetTestCase(true);
            var subject = new TestCaseValidatorDecrypt(testCase);
            var suppliedResult = GetTestCase(false);
            var result = await subject.ValidateAsync(suppliedResult);
            Assert.That(result != null);
            Assert.That(Disposition.Failed == result.Result);
            Assert.IsTrue(result.Reason.Contains("tag"));
        }

        [Test]
        public async Task ShouldNotFailTestDueToBadPlainTextWhenTestIsExpectedToBeFailureTest()
        {
            var testCase = GetTestCase(true);
            var subject = new TestCaseValidatorDecrypt(testCase);
            var suppliedResult = GetTestCase(true);
            suppliedResult.Plaintext = new BitString(0);
            var result = await subject.ValidateAsync(suppliedResult);
            Assert.That(result != null);
            Assert.AreEqual(Disposition.Passed, result.Result);
        }

        [Test]
        public async Task ShouldFailIfPlainTextNotPresent()
        {
            var testCase = GetTestCase();
            var subject = new TestCaseValidatorDecrypt(testCase);
            var suppliedResult = GetTestCase();

            suppliedResult.Plaintext = null;

            var result = await subject.ValidateAsync(suppliedResult);
            Assert.That(result != null);
            Assert.That(Disposition.Failed == result.Result);

            Assert.IsTrue(result.Reason.Contains($"{nameof(suppliedResult.Plaintext)} was not present in the {nameof(TestCase)}"));
        }

        private TestCase GetTestCase(bool failureTest = false)
        {
            var testCase = new TestCase
            {
                TestPassed = !failureTest,
                Plaintext = new BitString("ABCDEF0123456789ABCDEF0123456789"),
                TestCaseId = 1
            };
            return testCase;
        }
    }
}
