﻿using System.Collections.Generic;
using NIST.CVP.ACVTS.Libraries.Generation.Core;

namespace NIST.CVP.ACVTS.Libraries.Generation.ConditioningComponents.Sp800_90B.Hash_DF
{
    public class TestGroupGeneratorFactory : ITestGroupGeneratorFactory<Parameters, TestGroup, TestCase>
    {
        public IEnumerable<ITestGroupGeneratorAsync<Parameters, TestGroup, TestCase>> GetTestGroupGenerators(Parameters parameters)
        {
            var list = new HashSet<ITestGroupGeneratorAsync<Parameters, TestGroup, TestCase>>()
            {
                new TestGroupGenerator()
            };

            return list;
        }
    }
}
