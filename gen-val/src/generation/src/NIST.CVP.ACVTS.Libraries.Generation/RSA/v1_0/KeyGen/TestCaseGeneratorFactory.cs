﻿using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions;

namespace NIST.CVP.ACVTS.Libraries.Generation.RSA.v1_0.KeyGen
{
    public class TestCaseGeneratorFactory : ITestCaseGeneratorFactoryAsync<TestGroup, TestCase>
    {
        private readonly IOracle _oracle;

        public TestCaseGeneratorFactory(IOracle oracle)
        {
            _oracle = oracle;
        }

        public ITestCaseGeneratorAsync<TestGroup, TestCase> GetCaseGenerator(TestGroup testGroup)
        {
            return testGroup.TestType.ToLower() switch
            {
                "kat" => new TestCaseGeneratorKat(testGroup, _oracle),
                "aft" => new TestCaseGeneratorAft(_oracle),
                "gdt" => new TestCaseGeneratorGdt(_oracle),
                _ => new TestCaseGeneratorNull()
            };
        }
    }
}