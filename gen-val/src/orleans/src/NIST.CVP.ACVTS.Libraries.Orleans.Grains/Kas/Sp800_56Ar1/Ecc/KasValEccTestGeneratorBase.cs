﻿using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Builders;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF.KdfOneStep;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.NoKC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Scheme;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.DispositionTypes;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes.Kas.Sp800_56Ar1;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes.Kas.Sp800_56Ar1;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Kas.Sp800_56Ar1.Ecc.Helpers;

namespace NIST.CVP.ACVTS.Libraries.Orleans.Grains.Kas.Sp800_56Ar1.Ecc
{
    internal abstract class KasValEccTestGeneratorBase : KasValTestGeneratorBase<
    KasValParametersEcc, KasValResultEcc,
    KasDsaAlgoAttributesEcc, EccDomainParameters, EccKeyPair>
    {
        private readonly IEccCurveFactory _curveFactory;

        protected KasValEccTestGeneratorBase(
            IKasBuilder<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair
            > kasBuilder,
            ISchemeBuilder<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair
            > schemeBuilder,
            IEntropyProviderFactory entropyProviderFactory,
            IMacParametersBuilder macParametersBuilder,
            IKdfOneStepFactory kdfFactory,
            INoKeyConfirmationFactory noKeyConfirmationFactory,
            IKeyConfirmationFactory keyConfirmationFactory,
            IEccCurveFactory curveFactory,
            IShaFactory shaFactory
        ) : base(kasBuilder, schemeBuilder, entropyProviderFactory, macParametersBuilder, kdfFactory, noKeyConfirmationFactory, keyConfirmationFactory, shaFactory)
        {
            _curveFactory = curveFactory;
        }

        protected override EccDomainParameters GetDomainParameters(KasValParametersEcc param)
        {
            return new EccDomainParameters(_curveFactory.GetCurve(param.Curve));
        }

        protected override void MangleKeys(KasValResultEcc result, KasValTestDisposition intendedDisposition, IKas<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair> serverKas, IKas<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair> iutKas)
        {
            TestDispositionHelperEcc.MangleKeys(
                result,
                intendedDisposition,
                serverKas,
                iutKas
            );
        }

        protected override void SetResultInformationFromKasProcessing(KasValParametersEcc param, KasValResultEcc result, IKas<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair> serverKas, IKas<KasDsaAlgoAttributesEcc, OtherPartySharedInformation<EccDomainParameters, EccKeyPair>, EccDomainParameters, EccKeyPair> iutKas, KasResult iutResult)
        {
            TestDispositionHelperEcc.SetResultInformationFromKasProcessing(
                param,
                result,
                serverKas,
                iutKas,
                iutResult
            );
        }
    }
}
