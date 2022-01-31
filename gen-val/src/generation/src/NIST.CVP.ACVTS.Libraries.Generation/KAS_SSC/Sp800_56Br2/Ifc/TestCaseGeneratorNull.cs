﻿using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;

namespace NIST.CVP.ACVTS.Libraries.Generation.KAS_SSC.Sp800_56Br2.Ifc
{
    public class TestCaseGeneratorNull : ITestCaseGeneratorAsync<TestGroup, TestCase>
    {
        public int NumberOfTestCasesToGenerate => 0;
        public Task<TestCaseGenerateResponse<TestGroup, TestCase>> GenerateAsync(TestGroup @group, bool isSample, int caseNo = -1)
        {
            return Task.FromResult(new TestCaseGenerateResponse<TestGroup, TestCase>("This is the null generator -- nothing is generated"));
        }
    }
}
