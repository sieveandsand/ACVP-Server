﻿using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Domain;

namespace NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash
{
    public interface IShaMct
    {
        MctResult<AlgoArrayResponse> MctHash(BitString message, MathDomain domain, bool isSample);
    }
}
