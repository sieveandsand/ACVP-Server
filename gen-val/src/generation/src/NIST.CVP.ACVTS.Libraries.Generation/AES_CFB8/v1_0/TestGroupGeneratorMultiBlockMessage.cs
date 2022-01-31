﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Common.Helpers;
using NIST.CVP.ACVTS.Libraries.Generation.Core;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_CFB8.v1_0
{
    public class TestGroupGeneratorMultiBlockMessage : ITestGroupGeneratorAsync<Parameters, TestGroup, TestCase>
    {
        public const string MMT_TYPE_LABEL = "MMT";

        public Task<List<TestGroup>> BuildTestGroupsAsync(Parameters parameters)
        {
            var testGroups = new List<TestGroup>();
            var algoMode = AlgoModeHelpers.GetAlgoModeFromAlgoAndMode(parameters.Algorithm, parameters.Mode, parameters.Revision);

            foreach (var function in parameters.Direction)
            {
                foreach (var keyLength in parameters.KeyLen)
                {
                    var testGroup = new TestGroup
                    {
                        AlgoMode = algoMode,
                        Function = function,
                        KeyLength = keyLength,
                        InternalTestType = MMT_TYPE_LABEL
                    };
                    testGroups.Add(testGroup);
                }
            }
            return Task.FromResult(testGroups);
        }
    }
}
