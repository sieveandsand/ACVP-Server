﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.DRBG.Enums;
using NIST.CVP.ACVTS.Libraries.Generation.DRBG.v1_0;
using NIST.CVP.ACVTS.Libraries.Math.Domain;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.DRBG
{
    [TestFixture, UnitTest]
    public class ParameterValidatorTests
    {
        private ParameterValidator _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new ParameterValidator();
        }

        [Test]
        [TestCase(DrbgMechanism.Counter, DrbgMode.AES128)]
        [TestCase(DrbgMechanism.Counter, DrbgMode.AES192)]
        [TestCase(DrbgMechanism.Counter, DrbgMode.AES256)]
        public void ShouldValidateSuccessfullyAllValidAlgoAndModes(DrbgMechanism drbgMechanism, DrbgMode drbgMode)
        {
            Parameters p = new ParameterBuilder(drbgMechanism, drbgMode).Build();
            var result = _subject.Validate(p);

            Assert.IsTrue(result.Success, result.ErrorMessage);
        }

        [Test]
        public void ShouldFailValidationWithInvalidDrbgMode()
        {
            var i = -1;
            var invalid = (DrbgMode)i;

            Parameters p = new ParameterBuilder(DrbgMechanism.Counter, invalid).Build();
            var result = _subject.Validate(p);

            Assert.IsFalse(result.Success, result.ErrorMessage);
        }

        [Test]
        [TestCase("DefFunc test at seedlen+ ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, true, true)]
        [TestCase("DefFunc test at seedlen+ ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, true, true)]
        [TestCase("DefFunc test at seedlen+ ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, true, true)]
        [TestCase("Not DefFunc test at seedlen+ ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, false, false)]
        [TestCase("Not DefFunc test at seedlen+ ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, false, false)]
        [TestCase("Not DefFunc test at seedlen+ ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, false, false)]
        public void ShouldFailValidationWhenNotDerFuncAndEntropyNotSeedLen(string label, DrbgMechanism drbgMechanism, DrbgMode drbgMode, bool derFunc, bool expectedSuccess)
        {
            ParameterBuilder pb = new ParameterBuilder(drbgMechanism, drbgMode);

            MathDomain md = new MathDomain();
            md.AddSegment(new ValueDomainSegment(pb.SeedLength + 8));

            pb.WithDerFunctionEnabled(derFunc)
              .WithEntropyInputLen(md);
            Parameters p = pb.Build();

            var result = _subject.Validate(p);

            Assert.AreEqual(expectedSuccess, result.Success, result.ErrorMessage);
        }

        [Test]
        [TestCase("DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, true, false)]
        [TestCase("DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, true, false)]
        [TestCase("DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, true, false)]
        [TestCase("Not DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, false, true)]
        [TestCase("Not DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, false, true)]
        [TestCase("Not DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, false, true)]
        public void ShouldFailValidationWhenNotDerFuncAndNonceLtHalfSecurityStrength(string label, DrbgMechanism drbgMechanism, DrbgMode drbgMode, bool derFunc, bool expectedSuccess)
        {
            ParameterBuilder pb = new ParameterBuilder(drbgMechanism, drbgMode);

            MathDomain md = new MathDomain();
            md.AddSegment(new ValueDomainSegment(pb.SecurityStrength / 2 - 8));

            pb.WithDerFunctionEnabled(derFunc)
              .WithNonceLen(md);
            Parameters p = pb.Build();

            var result = _subject.Validate(p);

            Assert.AreEqual(expectedSuccess, result.Success, result.ErrorMessage);
        }

        [Test]
        [TestCase("DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, true, true)]
        [TestCase("DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, true, true)]
        [TestCase("DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, true, true)]
        [TestCase("Not DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, false, false)]
        [TestCase("Not DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, false, false)]
        [TestCase("Not DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, false, false)]
        public void ShouldFailValidationWhenNotDerFuncAndPersoStringNotSeedLen(string label, DrbgMechanism drbgMechanism, DrbgMode drbgMode, bool derFunc, bool expectedSuccess)
        {
            ParameterBuilder pb = new ParameterBuilder(drbgMechanism, drbgMode);

            MathDomain md = new MathDomain();
            md.AddSegment(new ValueDomainSegment(pb.SeedLength + 8));

            pb.WithDerFunctionEnabled(derFunc)
              .WithPersoStringLen(md);
            Parameters p = pb.Build();

            var result = _subject.Validate(p);

            Assert.AreEqual(expectedSuccess, result.Success, result.ErrorMessage);
        }

        [Test]
        [TestCase("DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, true, true)]
        [TestCase("DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, true, true)]
        [TestCase("DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, true, true)]
        [TestCase("Not DefFunc test at seedlen ctr aes128", DrbgMechanism.Counter, DrbgMode.AES128, false, false)]
        [TestCase("Not DefFunc test at seedlen ctr aes192", DrbgMechanism.Counter, DrbgMode.AES192, false, false)]
        [TestCase("Not DefFunc test at seedlen ctr aes256", DrbgMechanism.Counter, DrbgMode.AES256, false, false)]
        public void ShouldFailValidationWhenNotDerFuncAndAdditionalInputNotSeedLen(string label, DrbgMechanism drbgMechanism, DrbgMode drbgMode, bool derFunc, bool expectedSuccess)
        {
            ParameterBuilder pb = new ParameterBuilder(drbgMechanism, drbgMode);

            MathDomain md = new MathDomain();
            md.AddSegment(new ValueDomainSegment(pb.SeedLength + 8));

            pb.WithDerFunctionEnabled(derFunc)
              .WithAdditionalInputLen(md);
            Parameters p = pb.Build();

            var result = _subject.Validate(p);

            Assert.AreEqual(expectedSuccess, result.Success, result.ErrorMessage);
        }
    }
}
