﻿using System;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.Enums;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes;
using NLog;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_CTR.v1_0
{
    public class TestCaseGeneratorCounter : ITestCaseGeneratorWithPrep<TestGroup, TestCase>
    {
        private readonly IOracle _oracle;

        private int _numberOfBlocks = 1000;

        public int NumberOfTestCasesToGenerate { get; } = 1;

        public TestCaseGeneratorCounter(IOracle oracle)
        {
            _oracle = oracle;
        }

        public GenerateResponse PrepareGenerator(TestGroup group, bool isSample)
        {
            return new GenerateResponse();
        }

        public async Task<TestCaseGenerateResponse<TestGroup, TestCase>> GenerateAsync(TestGroup group, bool isSample, int caseNo = 0)
        {
            var param = new CounterParameters<AesParameters>
            {
                Parameters = new AesParameters
                {
                    Mode = BlockCipherModesOfOperation.Ctr,
                    Direction = group.Direction,
                    DataLength = 128 * _numberOfBlocks,
                    KeyLength = group.KeyLength
                },
                Overflow = group.OverflowCounter,
                Incremental = group.IncrementalCounter
            };

            try
            {
                AesResult result = null;

                if (isSample)
                {
                    // Generate full test case
                    result = await _oracle.CompleteDeferredAesCounterCaseAsync(param);
                }
                else
                {
                    // Generate partial test case
                    result = await _oracle.GetDeferredAesCounterCaseAsync(param);
                }

                return new TestCaseGenerateResponse<TestGroup, TestCase>(new TestCase
                {
                    Deferred = true,
                    CipherText = result.CipherText,
                    IV = result.Iv,
                    Key = result.Key,
                    PlainText = result.PlainText,
                    PayloadLength = result.PlainText?.BitLength ?? result.CipherText.BitLength
                });
            }
            catch (Exception ex)
            {
                ThisLogger.Error(ex);
                return new TestCaseGenerateResponse<TestGroup, TestCase>($"Failed to generate. {ex.Message}");
            }
        }

        private static ILogger ThisLogger => LogManager.GetCurrentClassLogger();
    }
}
