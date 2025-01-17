﻿using System.Linq;
using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Generation.AES_CCM.v1_0;
using NIST.CVP.ACVTS.Libraries.Generation.Tests;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Domain;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_CCM.IntegrationTests
{
    [TestFixture, FastIntegrationTest]
    public class GenValTests : GenValTestsSingleRunnerBase
    {
        public override string Algorithm { get; } = "ACVP-AES-CCM";
        public override string Mode { get; } = null;

        public override AlgoMode AlgoMode => AlgoMode.AES_CCM_v1_0;


        public override IRegisterInjections RegistrationsGenVal => new RegisterInjections();

        protected override void ModifyTestCaseToFail(dynamic testCase)
        {
            var rand = new Random800_90();

            // If TC is intended to be a failure test, change it
            if (testCase.testPassed != null)
            {
                testCase.testPassed = true;
            }

            // If TC has a cipherText, change it
            if (testCase.ct != null)
            {
                BitString bs = new BitString(testCase.ct.ToString());
                bs = rand.GetDifferentBitStringOfSameSize(bs);

                testCase.ct = bs.ToHex();
            }

            // If TC has a plainText, change it
            if (testCase.pt != null)
            {
                BitString bs = new BitString(testCase.pt.ToString());
                bs = rand.GetDifferentBitStringOfSameSize(bs);

                // Can't get something "different" of empty bitstring of the same length
                if (bs == null)
                {
                    bs = new BitString("01");
                }

                testCase.pt = bs.ToHex();
            }
        }

        protected override string GetTestFileFewTestCases(string targetFolder)
        {
            MathDomain ptDomain = new MathDomain();
            ptDomain.AddSegment(new ValueDomainSegment(0));

            MathDomain aadDomain = new MathDomain();
            aadDomain.AddSegment(new ValueDomainSegment(8));

            var tagLen = new[] { ParameterValidator.VALID_TAG_LENGTHS.First() };

            MathDomain nonceDomain = new MathDomain();
            nonceDomain.AddSegment(new ValueDomainSegment(ParameterValidator.VALID_NONCE_LENGTHS.First()));

            Parameters p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                KeyLen = new[] { ParameterValidator.VALID_KEY_SIZES.First() },
                PayloadLen = ptDomain,
                AadLen = aadDomain,
                TagLen = tagLen,
                IvLen = nonceDomain,
                IsSample = true
            };

            return CreateRegistration(targetFolder, p);
        }

        protected override string GetTestFileLotsOfTestCases(string targetFolder)
        {
            Random800_90 random = new Random800_90();

            MathDomain ptDomain = new MathDomain();
            ptDomain.AddSegment(new RangeDomainSegment(random, 0, 32 * 8, 8));

            MathDomain aadDomain = new MathDomain();
            aadDomain.AddSegment(new RangeDomainSegment(random, 0, (1 << 19), 8));

            var tagLen = ParameterValidator.VALID_TAG_LENGTHS;

            MathDomain nonceDomain = new MathDomain();
            foreach (var length in ParameterValidator.VALID_NONCE_LENGTHS)
            {
                nonceDomain.AddSegment(new ValueDomainSegment(length));
            }

            Parameters p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                KeyLen = ParameterValidator.VALID_KEY_SIZES,
                PayloadLen = ptDomain,
                AadLen = aadDomain,
                TagLen = tagLen,
                IvLen = nonceDomain,
                IsSample = false
            };

            return CreateRegistration(targetFolder, p);
        }
    }
}
