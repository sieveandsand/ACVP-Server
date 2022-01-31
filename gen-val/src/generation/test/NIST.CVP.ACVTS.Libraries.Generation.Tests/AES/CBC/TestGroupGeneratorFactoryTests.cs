﻿using System;
using System.Linq;
using NIST.CVP.ACVTS.Libraries.Generation.AES_CBC.v1_0;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.AES.CBC
{
    [TestFixture]
    public class TestGroupGeneratorFactoryTests
    {
        private TestGroupGeneratorFactory _subject;

        [Test]
        [TestCase(typeof(TestGroupGeneratorKnownAnswerTests))]
        [TestCase(typeof(TestGroupGeneratorMultiBlockMessage))]
        [TestCase(typeof(TestGroupGeneratorMonteCarlo))]
        [TestCase(typeof(TestGroupGeneratorHighAssuranceCryptoTest))]
        public void ReturnedResultShouldContainExpectedTypes(Type expectedType)
        {
            _subject = new TestGroupGeneratorFactory();

            var result = _subject.GetTestGroupGenerators(new Parameters());

            Assert.IsTrue(result.Count(w => w.GetType() == expectedType) == 1);
        }

        [Test]
        public void ReturnedResultShouldContainFourGenerators()
        {
            _subject = new TestGroupGeneratorFactory();

            var result = _subject.GetTestGroupGenerators(new Parameters());

            Assert.IsTrue(result.Count() == 4);
        }
    }
}
