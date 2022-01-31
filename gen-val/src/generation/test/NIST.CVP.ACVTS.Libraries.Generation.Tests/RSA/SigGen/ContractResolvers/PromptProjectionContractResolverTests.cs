﻿using System.Text.RegularExpressions;
using NIST.CVP.ACVTS.Libraries.Generation.Core.DeSerialization;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Enums;
using NIST.CVP.ACVTS.Libraries.Generation.Core.JsonConverters;
using NIST.CVP.ACVTS.Libraries.Generation.RSA.v1_0.SigGen;
using NIST.CVP.ACVTS.Libraries.Generation.RSA.v1_0.SigGen.ContractResolvers;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.RSA.SigGen.ContractResolvers
{
    [TestFixture, UnitTest, FastIntegrationTest]
    public class PromptProjectionContractResolverTests
    {
        private readonly JsonConverterProvider _jsonConverterProvider = new JsonConverterProvider();
        private readonly ContractResolverFactory _contractResolverFactory = new ContractResolverFactory();
        private readonly Projection _projection = Projection.Prompt;

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
        /// All group level properties are present in the prompt file
        /// </summary>
        [Test]
        public void ShouldSerializeGroupProperties()
        {
            var tvs = TestDataMother.GetTestGroups();
            var tg = tvs.TestGroups[0];

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];

            Assert.AreEqual(tg.TestGroupId, newTg.TestGroupId, nameof(newTg.TestGroupId));
            Assert.AreEqual(tg.Tests.Count, newTg.Tests.Count, nameof(newTg.Tests));
            Assert.AreEqual(tg.Modulo, newTg.Modulo, nameof(newTg.Modulo));
            Assert.AreEqual(tg.Mode, newTg.Mode, nameof(newTg.Mode));
            Assert.AreEqual(tg.SaltLen, newTg.SaltLen, nameof(newTg.SaltLen));
            Assert.AreEqual(tg.HashAlgName, newTg.HashAlgName, nameof(newTg.HashAlgName));

            Assert.AreNotEqual(tg.N, newTg.N, nameof(newTg.N));
            Assert.AreNotEqual(tg.E, newTg.E, nameof(newTg.E));

            Assert.IsFalse(newTg.IsMessageRandomized, nameof(newTg.IsMessageRandomized));
        }

        /// <summary>
        /// Encrypt test group should not contain the cipherText, results array, deferred, testPassed
        /// all other properties included
        /// </summary>
        [Test]
        public void ShouldSerializeCaseProperties()
        {
            var tvs = TestDataMother.GetTestGroups(1);
            var tg = tvs.TestGroups[0];
            var tc = tg.Tests[0];
            tc.RandomValue = new BitString(128);
            tc.RandomValueLen = tc.RandomValue.BitLength;

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));
            Assert.AreEqual(tc.Deferred, newTc.Deferred, nameof(newTc.Deferred));
            Assert.AreEqual(tc.Message, newTc.Message, nameof(newTc.Message));

            Assert.AreNotEqual(tc.Signature, newTc.Signature, nameof(newTc.Signature));

            Assert.IsNull(newTc.RandomValue, nameof(newTc.RandomValue));
            Assert.IsTrue(newTc.RandomValueLen == 0, nameof(newTc.RandomValueLen));

            // TestPassed will have the default value when re-hydrated, check to make sure it isn't in the JSON
            var regex = new Regex("testPassed", RegexOptions.IgnoreCase);
            Assert.IsTrue(regex.Matches(json).Count == 0);
        }

        /// <summary>
        /// All group level properties are present in the prompt file
        /// </summary>
        [Test]
        public void ShouldSerializeGroupPropertiesSp800_106()
        {
            var tvs = TestDataMother.GetTestGroups();
            var tg = tvs.TestGroups[0];
            tg.Conformance = "SP800-106";

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];

            Assert.AreEqual(tg.TestGroupId, newTg.TestGroupId, nameof(newTg.TestGroupId));
            Assert.AreEqual(tg.Tests.Count, newTg.Tests.Count, nameof(newTg.Tests));
            Assert.AreEqual(tg.Modulo, newTg.Modulo, nameof(newTg.Modulo));
            Assert.AreEqual(tg.Mode, newTg.Mode, nameof(newTg.Mode));
            Assert.AreEqual(tg.SaltLen, newTg.SaltLen, nameof(newTg.SaltLen));
            Assert.AreEqual(tg.HashAlgName, newTg.HashAlgName, nameof(newTg.HashAlgName));

            Assert.AreNotEqual(tg.N, newTg.N, nameof(newTg.N));
            Assert.AreNotEqual(tg.E, newTg.E, nameof(newTg.E));

            Assert.IsTrue(newTg.IsMessageRandomized, nameof(newTg.IsMessageRandomized));
        }

        /// <summary>
        /// Encrypt test group should not contain the cipherText, results array, deferred, testPassed
        /// all other properties included
        /// </summary>
        [Test]
        public void ShouldSerializeCasePropertiesSp800_106()
        {
            var tvs = TestDataMother.GetTestGroups(1);
            var tg = tvs.TestGroups[0];
            tg.Conformance = "SP800-106";
            var tc = tg.Tests[0];
            tc.RandomValue = new BitString(128);
            tc.RandomValueLen = tc.RandomValue.BitLength;

            var json = _serializer.Serialize(tvs, _projection);
            var newTvs = _deserializer.Deserialize(json);

            var newTg = newTvs.TestGroups[0];
            var newTc = newTg.Tests[0];

            Assert.AreEqual(tc.ParentGroup.TestGroupId, newTc.ParentGroup.TestGroupId, nameof(newTc.ParentGroup));
            Assert.AreEqual(tc.TestCaseId, newTc.TestCaseId, nameof(newTc.TestCaseId));
            Assert.AreEqual(tc.Deferred, newTc.Deferred, nameof(newTc.Deferred));
            Assert.AreEqual(tc.Message, newTc.Message, nameof(newTc.Message));

            Assert.AreNotEqual(tc.Signature, newTc.Signature, nameof(newTc.Signature));

            Assert.IsNull(newTc.RandomValue, nameof(newTc.RandomValue));
            Assert.IsTrue(newTc.RandomValueLen == 0, nameof(newTc.RandomValueLen));

            // TestPassed will have the default value when re-hydrated, check to make sure it isn't in the JSON
            var regex = new Regex("testPassed", RegexOptions.IgnoreCase);
            Assert.IsTrue(regex.Matches(json).Count == 0);
        }
    }
}
