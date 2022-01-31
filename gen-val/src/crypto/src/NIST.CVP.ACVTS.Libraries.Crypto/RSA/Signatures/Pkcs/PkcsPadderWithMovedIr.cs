﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.Signatures;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Crypto.RSA.Signatures.Pkcs
{
    public class PkcsPadderWithMovedIr : PkcsPadder
    {
        public PkcsPadderWithMovedIr(ISha sha) : base(sha) { }

        public override PaddingResult Pad(int nlen, BitString message)
        {
            if (message.BitLength < GetHashAlgId().BitLength + 11 * 8)
            {
                return new PaddingResult("Message length too short");
            }

            var H = Sha.HashMessage(message).Digest;
            var T = BitString.ConcatenateBits(GetHashAlgId(), H);

            var psLen = nlen - (GetHashAlgId().BitLength + Sha.HashFunction.OutputLen) - 24;
            var PS = BitString.Ones(psLen - 8);

            var EM = new BitString("00");
            EM = BitString.ConcatenateBits(EM, new BitString("01"));
            EM = BitString.ConcatenateBits(EM, PS);
            EM = BitString.ConcatenateBits(EM, new BitString("FE"));    // ERROR: Should be FF, but is missing a bit
            EM = BitString.ConcatenateBits(EM, new BitString("00"));
            EM = BitString.ConcatenateBits(EM, T);

            return new PaddingResult(EM);
        }
    }
}
