﻿using System.Text.RegularExpressions;
using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Generation.AES_GCM.v1_0;
using NIST.CVP.ACVTS.Libraries.Generation.AES_GCM.v1_0.ContractResolvers;
using NIST.CVP.ACVTS.Libraries.Generation.Core.DeSerialization;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Enums;
using NIST.CVP.ACVTS.Libraries.Generation.Core.JsonConverters;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.AES.GCM.ContractResolvers
{
    [TestFixture, UnitTest, FastIntegrationTest]
    public class ResultsProjectionContractResolverTests
    {
        private readonly JsonConverterProvider _jsonConverterProvider = new JsonConverterProvider();
        private readonly ContractResolverFactory _contractResolverFactory = new ContractResolverFactory();
        private readonly Projection _projection = Projection.Result;

        private VectorSetSerializer<TestVectorSet, TestGroup, TestCase> _serializer;
        private VectorSetDeserializer<TestVectorSet, TestGroup, TestCase> _deserializer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _serializer =
                new VectorSetSerializer<TestVectorSet, TestGroup, TestCase>(
                    _jsonConverterProvider,
                    _contractResolverFactory
                );
            _deserializer =
                new VectorSetDeserializer<TestVectorSet, TestGroup, TestCase>(
                    _jsonConverterProvider
                );
        }

        /// <summary>
        /// Only the groupId and tests should be present in the result file
        /// </summary>
        [Test]
        public void ShouldSerializeGroupProperties()
        {
            var tvs = TestDataMother.GetTestGroups(1, "encrypt", "external", false, true);
            var tg = tvs.TestGroups[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];

            Assert.AreEqual(tg.TestGroupId, newTg.TestGroupId, nameof(newTg.TestGroupId));
            Assert.AreEqual(tg.Tests.Count, newTg.Tests.Count, nameof(newTg.Tests));

            Assert.AreNotEqual(tg.Function, newTg.Function, nameof(newTg.Function));
            Assert.AreNotEqual(tg.KeyLength, newTg.KeyLength, nameof(newTg.KeyLength));
            Assert.AreNotEqual(tg.AadLength, newTg.AadLength, nameof(newTg.AadLength));
            Assert.AreNotEqual(tg.IvLength, newTg.IvLength, nameof(newTg.IvLength));
            Assert.AreNotEqual(tg.TagLength, newTg.TagLength, nameof(newTg.TagLength));
            Assert.AreNotEqual(tg.PayloadLength, newTg.PayloadLength, nameof(newTg.PayloadLength));
        }

        /// <summary>
        /// Encrypt test group should contain the cipherText
        /// all other properties excluded
        /// </summary>
        /// <param name="function">The function being tested</param>
        /// <param name="ivGen">IV generation (internal/external from perspective of IUT)</param>
        /// <param name="deferred">Is this a deferred test? (Internal IV generation encrypt)</param>
        [Test]
        [TestCase("encrypt", "external", false)]
        [TestCase("encrypt", "internal", true)]
        public void ShouldSerializeEncryptCaseProperties(string function, string ivGen, bool deferred)
        {
            var tvs = TestDataMother.GetTestGroups(1, function, ivGen, deferred, true);
            var tg = tvs.TestGroups[0];
            var tc = tg.Tests[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));

            // not included in results file
            Assert.AreNotEqual(tc.Key, newTc.Key, nameof(newTc.Key));
            Assert.AreNotEqual(tc.PlainText, newTc.PlainText, nameof(newTc.PlainText));

            if (ivGen == "internal")
            {
                Assert.IsNotNull(newTc.CipherText, nameof(newTc.CipherText));
                Assert.IsNotNull(newTc.Tag, nameof(newTc.Tag));
                Assert.IsNotNull(newTc.IV, nameof(newTc.IV));
            }
            else
            {
                Assert.IsNotNull(newTc.CipherText, nameof(newTc.CipherText));
                Assert.IsNotNull(newTc.Tag, nameof(newTc.Tag));
                Assert.IsNull(newTc.IV, nameof(newTc.IV));
            }

            Regex regexDeferred = new Regex(nameof(TestCase.Deferred), RegexOptions.IgnoreCase);
            Assert.IsTrue(regexDeferred.Matches(json).Count == 0);

            // TestPassed will have the default value when re-hydrated, check to make sure it isn't in the JSON
            Regex regex = new Regex(nameof(TestCase.TestPassed), RegexOptions.IgnoreCase);
            Assert.IsTrue(regex.Matches(json).Count == 0);
        }

        /// <summary>
        /// Decrypt test group should contain the plainText, results array (when mct)
        /// all other properties excluded
        /// </summary>
        /// <param name="function">The function being tested</param>
        /// <param name="testPassed">The testType</param>
        [Test]
        [TestCase("decrypt", true)]
        [TestCase("decrypt", false)]
        public void ShouldSerializeDecryptCaseProperties(string function, bool testPassed)
        {
            var tvs = TestDataMother.GetTestGroups(1, function, "external", false, testPassed);
            var tg = tvs.TestGroups[0];
            var tc = tg.Tests[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));

            if (testPassed)
            {
                Assert.AreEqual(tc.PlainText, newTc.PlainText, nameof(newTc.PlainText));
                Regex regexPass = new Regex(nameof(TestCase.TestPassed), RegexOptions.IgnoreCase);
                Assert.IsTrue(regexPass.Matches(json).Count == 0, nameof(regexPass));
            }
            else
            {
                Assert.AreEqual(tc.TestPassed, newTc.TestPassed, nameof(newTc.TestPassed));

                Regex regexFail = new Regex(nameof(TestCase.TestPassed), RegexOptions.IgnoreCase);
                Assert.IsTrue(regexFail.Matches(json).Count > 0, nameof(regexFail));
            }

            // not included in results file
            Assert.AreNotEqual(tc.IV, newTc.IV, nameof(newTc.IV));
            Assert.AreNotEqual(tc.Key, newTc.Key, nameof(newTc.Key));
            Assert.AreNotEqual(tc.CipherText, newTc.CipherText, nameof(newTc.CipherText));

            Regex regexDeferred = new Regex(nameof(TestCase.Deferred), RegexOptions.IgnoreCase);
            Assert.IsTrue(regexDeferred.Matches(json).Count == 0);

            // TestPassed will have the default value when re-hydrated, check to make sure it isn't in the JSON
            if (testPassed)
            {
                Regex regexTestPassed = new Regex(nameof(TestCase.TestPassed), RegexOptions.IgnoreCase);
                Assert.IsTrue(regexTestPassed.Matches(json).Count == 0);
            }
            else
            {
                Assert.AreEqual(tc.TestPassed, newTc.TestPassed, nameof(newTc.TestPassed));
                Assert.IsFalse(newTc.TestPassed, nameof(newTc.TestPassed));
            }

        }

        [Test]
        public void ShouldSerializeGroupPropertiesGmac()
        {
            var tvs = TestDataMother.GetTestGroups(1, "encrypt", "external", false, true);
            var tg = tvs.TestGroups[0];
            tg.AlgoMode = AlgoMode.AES_GMAC_v1_0;
            tg.PayloadLength = 1; // doing this to ensure it gets serialized back to default

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];

            Assert.AreEqual(tg.TestGroupId, newTg.TestGroupId, nameof(newTg.TestGroupId));
            Assert.AreEqual(tg.Tests.Count, newTg.Tests.Count, nameof(newTg.Tests));

            Assert.AreNotEqual(tg.Function, newTg.Function, nameof(newTg.Function));
            Assert.AreNotEqual(tg.KeyLength, newTg.KeyLength, nameof(newTg.KeyLength));
            Assert.AreNotEqual(tg.AadLength, newTg.AadLength, nameof(newTg.AadLength));
            Assert.AreNotEqual(tg.IvLength, newTg.IvLength, nameof(newTg.IvLength));
            Assert.AreNotEqual(tg.TagLength, newTg.TagLength, nameof(newTg.TagLength));
            Assert.AreNotEqual(tg.PayloadLength, newTg.PayloadLength, nameof(newTg.PayloadLength));
        }

        /// <summary>
        /// Encrypt test group should contain the cipherText
        /// all other properties excluded
        /// </summary>
        /// <param name="function">The function being tested</param>
        /// <param name="ivGen">IV generation (internal/external from perspective of IUT)</param>
        /// <param name="deferred">Is this a deferred test? (Internal IV generation encrypt)</param>
        [Test]
        [TestCase("encrypt", "external", false)]
        [TestCase("encrypt", "internal", true)]
        public void ShouldSerializeEncryptCasePropertiesGmac(string function, string ivGen, bool deferred)
        {
            var tvs = TestDataMother.GetTestGroups(1, function, ivGen, deferred, true);
            var tg = tvs.TestGroups[0];
            tg.AlgoMode = AlgoMode.AES_GMAC_v1_0;
            var tc = tg.Tests[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));

            // not included in results file
            Assert.AreNotEqual(tc.Key, newTc.Key, nameof(newTc.Key));
            Assert.IsNull(newTc.PlainText, nameof(newTc.PlainText));
            Assert.IsNull(newTc.CipherText, nameof(newTc.CipherText));

            if (ivGen == "internal")
            {
                Assert.IsNotNull(newTc.Tag, nameof(newTc.Tag));
                Assert.IsNotNull(newTc.IV, nameof(newTc.IV));
            }
            else
            {
                Assert.IsNotNull(newTc.Tag, nameof(newTc.Tag));
                Assert.IsNull(newTc.IV, nameof(newTc.IV));
            }

            Regex regexDeferred = new Regex(nameof(TestCase.Deferred), RegexOptions.IgnoreCase);
            Assert.IsTrue(regexDeferred.Matches(json).Count == 0);

            // TestPassed will have the default value when re-hydrated, check to make sure it isn't in the JSON
            Regex regex = new Regex(nameof(TestCase.TestPassed), RegexOptions.IgnoreCase);
            Assert.IsTrue(regex.Matches(json).Count == 0);
        }

        /// <summary>
        /// Decrypt test group should contain the plainText, results array (when mct)
        /// all other properties excluded
        /// </summary>
        /// <param name="function">The function being tested</param>
        /// <param name="testPassed">The testType</param>
        [Test]
        [TestCase("decrypt", true)]
        [TestCase("decrypt", false)]
        public void ShouldSerializeDecryptCasePropertiesGmac(string function, bool testPassed)
        {
            var tvs = TestDataMother.GetTestGroups(1, function, "external", false, testPassed);
            var tg = tvs.TestGroups[0];
            tg.AlgoMode = AlgoMode.AES_GMAC_v1_0;
            var tc = tg.Tests[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));
            Assert.IsNull(newTc.PlainText, nameof(newTc.PlainText));
            Assert.IsNull(newTc.CipherText, nameof(newTc.CipherText));

            // not included in results file
            Assert.AreNotEqual(tc.IV, newTc.IV, nameof(newTc.IV));
            Assert.AreNotEqual(tc.Key, newTc.Key, nameof(newTc.Key));

            Regex regexDeferred = new Regex(nameof(TestCase.Deferred), RegexOptions.IgnoreCase);
            Assert.IsTrue(regexDeferred.Matches(json).Count == 0);

            Assert.AreEqual(tc.TestPassed, newTc.TestPassed, nameof(newTc.TestPassed));
        }
    }
}
