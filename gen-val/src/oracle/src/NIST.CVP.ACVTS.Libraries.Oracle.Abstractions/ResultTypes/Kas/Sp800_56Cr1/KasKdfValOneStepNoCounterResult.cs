﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.FixedInfo;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF.KdfOneStep;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes.Kas.Sp800_56Cr1
{
    public class KdaValOneStepNoCounterResult
    {
        public KdfParameterOneStepNoCounter KdfInputs { get; set; }
        public PartyFixedInfo FixedInfoPartyU { get; set; }
        public PartyFixedInfo FixedInfoPartyV { get; set; }
        public BitString DerivedKeyingMaterial { get; set; }
        public bool TestPassed { get; set; }
    }
}