﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Common.ExtensionMethods;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.TDES;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.TDES.KATs;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Generation.TDES_CFBP.v1_0
{
    public class TestCaseGeneratorKat : ITestCaseGeneratorAsync<TestGroup, TestCase>
    {
        private readonly List<AlgoArrayResponse> _kats;

        private readonly Dictionary<string, List<AlgoArrayResponse>> _katMapping =
            new Dictionary<string, List<AlgoArrayResponse>>()
            {
                {"permutation", KatData.GetPermutationData()},
                {"inversepermutation", KatData.GetInversePermutationData()},
                {"substitutiontable", KatData.GetSubstitutionTableData()},
                {"variablekey", KatData.GetVariableKeyData()},
                {"variabletext", KatData.GetVariableTextData()}
            };

        private int _katsIndex;

        public int NumberOfTestCasesToGenerate => _kats.Count;

        public TestCaseGeneratorKat(string katType, AlgoMode mode)
        {
            if (!_katMapping
                .TryFirst(w => w.Key.Equals(katType, StringComparison.OrdinalIgnoreCase),
                    out var result)
            )
            {
                throw new ArgumentException($"Invalid {nameof(katType)}");
            }

            _kats = result.Value;
            _kats.ForEach(fe =>
            {
                fe.IV = fe.PlainText.GetDeepCopy();

                if (mode == AlgoMode.TDES_CFB1_v1_0 || mode == AlgoMode.TDES_CFBP1_v1_0)
                {
                    fe.CipherText = fe.CipherText.GetMostSignificantBits(1);
                }

                if (mode == AlgoMode.TDES_CFB8_v1_0 || mode == AlgoMode.TDES_CFBP8_v1_0)
                {
                    fe.CipherText = fe.CipherText.GetMostSignificantBits(8);
                }

                var len = 64;
                if (mode == AlgoMode.TDES_CFB1_v1_0 || mode == AlgoMode.TDES_CFBP1_v1_0)
                {
                    len = 1;
                }

                if (mode == AlgoMode.TDES_CFB8_v1_0 || mode == AlgoMode.TDES_CFBP8_v1_0)
                {
                    len = 8;
                }

                fe.PlainText = BitString.Zeroes(len);
            });
        }

        public async Task<TestCaseGenerateResponse<TestGroup, TestCase>> GenerateAsync(TestGroup group, bool isSample, int caseNo = 0)
        {
            if (_katsIndex + 1 > _kats.Count)
            {
                return await Task.FromResult(
                    new TestCaseGenerateResponse<TestGroup, TestCase>("No additional KATs exist."));
            }

            var currentKat = _kats[_katsIndex++];
            var testCase = new TestCase
            {
                Key1 = currentKat.Key1,
                Key2 = currentKat.Key2,
                Key3 = currentKat.Key3,
                PlainText = currentKat.PlainText,
                CipherText = currentKat.CipherText,
                IV = currentKat.IV
            };

            return await Task.FromResult(new TestCaseGenerateResponse<TestGroup, TestCase>(testCase));
        }
    }
}
