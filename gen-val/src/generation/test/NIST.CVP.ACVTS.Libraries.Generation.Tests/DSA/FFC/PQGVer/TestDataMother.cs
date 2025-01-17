﻿using System.Collections.Generic;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.FFC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.FFC.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper;
using NIST.CVP.ACVTS.Libraries.Generation.DSA.v1_0.PqgVer;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.DSA.FFC.PQGVer
{
    public static class TestDataMother
    {
        public static TestVectorSet GetTestGroups(int groups, GeneratorGenMode gGenMode, PrimeGenMode pqGenMode)
        {
            var vectorSet = new TestVectorSet
            {
                Algorithm = "DSA",
                Mode = "PQGVer"
            };

            var testGroups = new List<TestGroup>();
            vectorSet.TestGroups = testGroups;
            for (var groupIdx = 0; groupIdx < groups; groupIdx++)
            {
                TestGroup tg = new TestGroup
                {
                    GGenMode = gGenMode,
                    PQGenMode = pqGenMode,
                    HashAlg = new HashFunction(ModeValues.SHA2, DigestSizes.d256),
                    L = 2048,
                    N = 256,
                    TestType = "gdt",
                    TestGroupId = groupIdx,
                };
                testGroups.Add(tg);

                var tests = new List<TestCase>();
                tg.Tests = tests;
                for (var testId = 5 * groupIdx + 1; testId <= (groupIdx + 1) * 5; testId++)
                {
                    tests.Add(new TestCase
                    {
                        P = BitString.To32BitString(1),
                        Q = BitString.To32BitString(2),
                        G = BitString.To32BitString(3),
                        H = 4,
                        Reason = "none",
                        TestPassed = true,
                        Seed = new DomainSeed(BitString.Ones(248).ToPositiveBigInteger(), BitString.Ones(248).ToPositiveBigInteger(), BitString.Ones(248).ToPositiveBigInteger()),
                        Counter = new Counter(5, 88),
                        Index = new BitString("ABCD"),
                        TestCaseId = testId,
                        ParentGroup = tg
                    });
                }
            }

            return vectorSet;
        }
    }
}
