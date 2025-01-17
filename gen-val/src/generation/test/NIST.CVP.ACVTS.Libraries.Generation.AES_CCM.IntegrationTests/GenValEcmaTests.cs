﻿using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Generation.AES_CCM.v1_0;
using NIST.CVP.ACVTS.Libraries.Generation.Tests;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Domain;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_CCM.IntegrationTests
{
    [TestFixture, FastIntegrationTest]
    public class GenValEcmaTests : GenValTestsSingleRunnerBase
    {
        public override string Algorithm { get; } = "ACVP-AES-CCM";
        public override string Mode { get; } = "ECMA";

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
            var ptDomain = new MathDomain();
            ptDomain.AddSegment(new ValueDomainSegment(14 * 8));

            var aadDomain = new MathDomain();
            aadDomain.AddSegment(new ValueDomainSegment(16 * 8));

            var tagLen = new[] { 8 * 8 };

            var nonceDomain = new MathDomain();
            nonceDomain.AddSegment(new ValueDomainSegment(13 * 8));

            var p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                KeyLen = new[] { 128 },
                PayloadLen = ptDomain,
                AadLen = aadDomain,
                TagLen = tagLen,
                IvLen = nonceDomain,
                Conformances = new[] { "ECMA" },
                IsSample = true
            };

            return CreateRegistration(targetFolder, p);
        }

        protected override string GetTestFileLotsOfTestCases(string targetFolder)
        {
            var random = new Random800_90();

            var ptDomain = new MathDomain();
            ptDomain.AddSegment(new RangeDomainSegment(random, 14 * 8, 4095 * 8, 8));

            var aadDomain = new MathDomain();
            aadDomain.AddSegment(new RangeDomainSegment(random, 14 * 8, 4109 * 8, 8));

            var tagLen = new[] { 8 * 8 };

            var nonceDomain = new MathDomain();
            nonceDomain.AddSegment(new ValueDomainSegment(13 * 8));

            var p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                KeyLen = new[] { 128 },
                PayloadLen = ptDomain,
                AadLen = aadDomain,
                TagLen = tagLen,
                IvLen = nonceDomain,
                Conformances = new[] { "ECMA" },
                IsSample = false
            };

            return CreateRegistration(targetFolder, p);
        }
    }
}
