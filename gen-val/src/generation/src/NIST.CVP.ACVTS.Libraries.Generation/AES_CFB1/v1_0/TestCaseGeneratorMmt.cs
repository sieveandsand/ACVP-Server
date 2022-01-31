﻿using System;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.Enums;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes;
using NLog;

namespace NIST.CVP.ACVTS.Libraries.Generation.AES_CFB1.v1_0
{
    public class TestCaseGeneratorMmt : ITestCaseGeneratorAsync<TestGroup, TestCase>
    {
        private readonly IOracle _oracle;

        private const int LENGTH_MULTIPLIER = 1;

        private int _lenGenIteration = 1;

        public int NumberOfTestCasesToGenerate => 10;

        public TestCaseGeneratorMmt(IOracle oracle)
        {
            _oracle = oracle;
        }

        public async Task<TestCaseGenerateResponse<TestGroup, TestCase>> GenerateAsync(TestGroup group, bool isSample, int caseNo = 0)
        {
            var dataLength = _lenGenIteration++ * LENGTH_MULTIPLIER;

            var param = new AesParameters
            {
                Mode = BlockCipherModesOfOperation.CfbBit,
                DataLength = dataLength,
                Direction = group.Function,
                KeyLength = group.KeyLength
            };

            try
            {
                var oracleResult = await _oracle.GetAesCaseAsync(param);

                return new TestCaseGenerateResponse<TestGroup, TestCase>(new TestCase
                {
                    PayloadLen = dataLength,
                    Key = oracleResult.Key,
                    IV = oracleResult.Iv,
                    PlainText = oracleResult.PlainText,
                    CipherText = oracleResult.CipherText
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
