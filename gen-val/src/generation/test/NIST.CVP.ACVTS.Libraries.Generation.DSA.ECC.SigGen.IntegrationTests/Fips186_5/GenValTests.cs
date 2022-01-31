﻿using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Generation.ECDSA.v1_0.SigGen;
using NIST.CVP.ACVTS.Libraries.Generation.Tests;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Tests.Core.TestCategoryAttributes;
using NUnit.Framework;
using RegisterInjections = NIST.CVP.ACVTS.Libraries.Generation.ECDSA.Fips186_5.SigGen.RegisterInjections;

namespace NIST.CVP.ACVTS.Libraries.Generation.DSA.ECC.SigGen.IntegrationTests.Fips186_5
{
    [TestFixture, LongRunningIntegrationTest]
    public class GenValTests : GenValTestsSingleRunnerBase
    {
        public override string Algorithm { get; } = "ECDSA";
        public override string Mode { get; } = "SigGen";
        public override string Revision { get; set; } = "FIPS186-5";

        public override AlgoMode AlgoMode => AlgoMode.ECDSA_SigGen_Fips186_5;

        public override IRegisterInjections RegistrationsGenVal => new RegisterInjections();

        protected override string GetTestFileFewTestCases(string targetFolder)
        {
            var caps = new[]
            {
                new Capability
                {
                    Curve = new[] { "P-224" },
                    HashAlg = new[] { "SHA2-224" }
                },
                new Capability
                {
                    Curve = new[] { "P-521" },
                    HashAlg = new[] {"SHA2-512" }
                }
            };

            var p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                IsSample = true,
                Capabilities = caps,
                ComponentTest = true
            };

            return CreateRegistration(targetFolder, p);
        }

        protected override string GetTestFileLotsOfTestCases(string targetFolder)
        {
            var caps = new[]
            {
                new Capability
                {
                    Curve = ECDSA.Fips186_5.SigGen.ParameterValidator.VALID_CURVES,
                    HashAlg = ECDSA.Fips186_5.SigGen.ParameterValidator.VALID_HASH_ALGS
                }
            };

            var p = new Parameters
            {
                Algorithm = Algorithm,
                Mode = Mode,
                Revision = Revision,
                IsSample = true,
                Conformances = new[] { "SP800-106" },
                Capabilities = caps,
            };

            return CreateRegistration(targetFolder, p);
        }

        protected override void ModifyTestCaseToFail(dynamic testCase)
        {
            var rand = new Random800_90();
            if (testCase.r != null)
            {
                testCase.r = rand.GetDifferentBitStringOfSameSize(new BitString(testCase.r.ToString())).ToHex();
            }

            if (testCase.s != null)
            {
                testCase.s = rand.GetDifferentBitStringOfSameSize(new BitString(testCase.s.ToString())).ToHex();
            }
        }
    }
}
