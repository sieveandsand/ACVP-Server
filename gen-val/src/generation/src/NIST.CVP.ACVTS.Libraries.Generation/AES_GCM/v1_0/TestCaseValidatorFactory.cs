﻿using System.Collections.Generic;
using System.Linq;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_GCM.v1_0
{
    public class TestCaseValidatorFactory : ITestCaseValidatorFactoryAsync<TestVectorSet, TestGroup, TestCase>
    {
        private readonly IOracle _oracle;

        public TestCaseValidatorFactory(IOracle oracle)
        {
            _oracle = oracle;
        }

        public List<ITestCaseValidatorAsync<TestGroup, TestCase>> GetValidators(TestVectorSet testVectorSet)
        {
            var list = new List<ITestCaseValidatorAsync<TestGroup, TestCase>>();

            foreach (var group in testVectorSet.TestGroups.Select(g => g))
            {
                foreach (var test in group.Tests.Select(t => t))
                {
                    var workingTest = test;
                    if (test.Deferred && group.Function == "encrypt")
                    {
                        list.Add(
                            new TestCaseValidatorDeferredEncrypt(
                                group,
                                workingTest,
                                new DeferredEncryptResolver(_oracle)
                            )
                        );
                    }
                    else if (group.Function == "encrypt")
                    {
                        list.Add(new TestCaseValidatorEncrypt(group, workingTest));
                    }
                    else
                    {
                        list.Add(new TestCaseValidatorDecrypt(group, workingTest));
                    }

                }
            }

            return list;
        }
    }
}
