﻿using System.Numerics;
using Newtonsoft.Json;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC.Helpers;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.DispositionTypes;

namespace NIST.CVP.ACVTS.Libraries.Generation.ECDSA.v1_0.KeyVer
{
    public class TestCase : ITestCase<TestGroup, TestCase>
    {
        public int TestCaseId { get; set; }
        public bool? TestPassed { get; set; }
        [JsonIgnore]
        public bool Deferred => false;
        public TestGroup ParentGroup { get; set; }
        public EcdsaKeyDisposition Reason { get; set; }

        private int DegreeOfPolynomial => ParentGroup == null ? 0 : CurveAttributesHelper.GetCurveAttribute(ParentGroup.Curve).DegreeOfPolynomial;

        [JsonIgnore] public EccKeyPair KeyPair { get; set; } = new EccKeyPair();
        [JsonProperty(PropertyName = "d", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BitString D
        {
            get => KeyPair.PrivateD != 0 ? new BitString(KeyPair.PrivateD, DegreeOfPolynomial).PadToModulusMsb(BitString.BITSINBYTE) : null;
            set => KeyPair.PrivateD = value.ToPositiveBigInteger();
        }

        [JsonProperty(PropertyName = "qx", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BitString Qx
        {
            get => KeyPair.PublicQ.X != 0 ? new BitString(KeyPair.PublicQ.X, DegreeOfPolynomial).PadToModulusMsb(BitString.BITSINBYTE) : null;
            set => KeyPair.PublicQ.X = value.ToPositiveBigInteger();
        }

        [JsonProperty(PropertyName = "qy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BitString Qy
        {
            get => KeyPair.PublicQ.Y != 0 ? new BitString(KeyPair.PublicQ.Y, DegreeOfPolynomial).PadToModulusMsb(BitString.BITSINBYTE) : null;
            set => KeyPair.PublicQ.Y = value.ToPositiveBigInteger();
        }

        public bool SetString(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            // Sometimes these values aren't even length...
            if (value.Length % 2 != 0)
            {
                value = value.Insert(0, "0");
            }

            switch (name.ToLower())
            {
                case "qx":
                    KeyPair.PublicQ.X = new BitString(value).ToPositiveBigInteger();
                    return true;

                case "qy":
                    KeyPair.PublicQ.Y = new BitString(value).ToPositiveBigInteger();
                    return true;

                case "result":
                    TestPassed = value.Contains("p (0");
                    return true;
            }

            return false;
        }
    }
}
