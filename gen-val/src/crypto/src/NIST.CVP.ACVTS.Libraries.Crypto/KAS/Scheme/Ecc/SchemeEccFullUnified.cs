﻿using System;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.FixedInfo;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF.KdfOneStep;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.NoKC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Scheme;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KES;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;

namespace NIST.CVP.ACVTS.Libraries.Crypto.KAS.Scheme.Ecc
{
    public class SchemeEccFullUnified : SchemeBaseEcc
    {
        private readonly IDiffieHellman<EccDomainParameters, EccKeyPair> _diffieHellman;

        public SchemeEccFullUnified(
            IDsaEcc dsa,
            IEccCurveFactory curveFactory,
            IKdfOneStepFactory kdfFactory,
            IKeyConfirmationFactory keyConfirmationFactory,
            INoKeyConfirmationFactory noKeyConfirmationFactory,
            IOtherInfoFactory otherInfoFactory,
            IEntropyProvider entropyProvider,
            SchemeParametersBase<KasDsaAlgoAttributesEcc> schemeParameters,
            KdfParameters kdfParameters,
            MacParameters macParameters,
            IDiffieHellman<EccDomainParameters, EccKeyPair> diffieHellman
        ) :
            base(
                dsa,
                curveFactory,
                kdfFactory,
                keyConfirmationFactory,
                noKeyConfirmationFactory,
                otherInfoFactory,
                entropyProvider,
                schemeParameters,
                kdfParameters,
                macParameters
            )
        {
            _diffieHellman = diffieHellman;

            if (SchemeParameters.KasAlgoAttributes.Scheme != EccScheme.FullUnified)
            {
                throw new ArgumentException(nameof(SchemeParameters.KasAlgoAttributes.Scheme));
            }
        }

        protected override BitString ComputeSharedSecret(OtherPartySharedInformation<EccDomainParameters, EccKeyPair> otherPartyInformation)
        {
            var staticSecret = _diffieHellman.GenerateSharedSecretZ(
                DomainParameters,
                StaticKeyPair,
                otherPartyInformation.StaticPublicKey
            ).SharedSecretZ;

            var ephemeralSecret = _diffieHellman.GenerateSharedSecretZ(
                DomainParameters,
                EphemeralKeyPair,
                otherPartyInformation.EphemeralPublicKey
            ).SharedSecretZ;

            // Shared secret is composed (Z_e || Z_s)
            return BitString.ConcatenateBits(ephemeralSecret, staticSecret);
        }

        protected override void GenerateKasKeyNonceInformation()
        {
            if (DomainParameters == null)
            {
                GenerateDomainParameters();
            }

            // party U and party V for this scheme contribute both static and ephemeral keys
            StaticKeyPair = Dsa.GenerateKeyPair(DomainParameters).KeyPair;
            EphemeralKeyPair = Dsa.GenerateKeyPair(DomainParameters).KeyPair;

            // when party U and KdfNoKc, a NoKeyConfirmationNonce is needed.
            if (SchemeParameters.KeyAgreementRole == KeyAgreementRole.InitiatorPartyU
                && SchemeParameters.KasMode == KasMode.KdfNoKc)
            {
                NoKeyConfirmationNonce = EntropyProvider.GetEntropy(128);
            }
        }
    }
}
