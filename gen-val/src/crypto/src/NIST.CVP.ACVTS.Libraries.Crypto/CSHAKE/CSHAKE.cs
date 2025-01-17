﻿using System;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.CSHAKE;
using NIST.CVP.ACVTS.Libraries.Math;
using NLog;

namespace NIST.CVP.ACVTS.Libraries.Crypto.CSHAKE
{
    public class CSHAKE : ICSHAKE
    {
        private readonly ICSHAKEFactory _iCSHAKEFactory;

        public CSHAKE(ICSHAKEFactory iCSHAKEFactory)
        {
            _iCSHAKEFactory = iCSHAKEFactory;
        }

        public CSHAKE()
        {
            _iCSHAKEFactory = new CSHAKEFactory();
        }

        public HashResult HashMessage(HashFunction hashFunction, BitString message, string customization, string functionName = "")
        {
            try
            {
                var sha = _iCSHAKEFactory.GetCSHAKE(hashFunction);
                var digest = sha.HashMessage(message, hashFunction.DigestLength, hashFunction.Capacity, customization, functionName);

                return new HashResult(digest);
            }
            catch (Exception ex)
            {
                ThisLogger.Error(ex);
                return new HashResult(ex.Message);
            }
        }

        #region BitString Customization
        public HashResult HashMessage(HashFunction hashFunction, BitString message, BitString customization, string functionName = "")
        {
            try
            {
                var sha = _iCSHAKEFactory.GetCSHAKE(hashFunction);
                var digest = sha.HashMessage(message, hashFunction.DigestLength, hashFunction.Capacity, customization, functionName);

                return new HashResult(digest);
            }
            catch (Exception ex)
            {
                ThisLogger.Error(ex);
                return new HashResult(ex.Message);
            }
        }
        #endregion BitString Customization

        private Logger ThisLogger { get { return LogManager.GetCurrentClassLogger(); } }
    }
}
