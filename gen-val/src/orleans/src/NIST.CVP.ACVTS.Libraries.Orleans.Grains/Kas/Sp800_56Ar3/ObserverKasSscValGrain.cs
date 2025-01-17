﻿using System;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.FFC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Hash.ShaWrapper.Helpers;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Enums;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.FixedInfo;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KC;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Sp800_56Ar3;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Sp800_56Ar3.Builders;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Sp800_56Ar3.Enums;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;
using NIST.CVP.ACVTS.Libraries.Math.Helpers;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.DispositionTypes;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes.Kas.Sp800_56Ar3;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes.Kas.Sp800_56Ar3;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Interfaces.Exceptions;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Interfaces.Kas.Sp800_56Ar3;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Kas.Sp800_56Ar3.Helpers;

namespace NIST.CVP.ACVTS.Libraries.Orleans.Grains.Kas.Sp800_56Ar3
{
    public class ObserverKasSscValGrain : ObservableOracleGrainBase<KasSscValResult>,
        IObserverKasSscValGrain
    {
        private readonly IKasBuilder _kasBuilder;
        private readonly ISchemeBuilder _schemeBuilder;
        private readonly ISecretKeyingMaterialBuilder _serverSecretKeyingMaterialBuilder;
        private readonly ISecretKeyingMaterialBuilder _iutSecretKeyingMaterialBuilder;
        private readonly IEntropyProvider _entropyProvider;
        private readonly IRandom800_90 _random;
        private readonly IDsaEccFactory _dsaEccFactory;
        private readonly IDsaFfcFactory _dsaFfcFactory;
        private readonly IShaFactory _shaFactory;

        private KasSscValParameters _param;

        public ObserverKasSscValGrain(
            LimitedConcurrencyLevelTaskScheduler nonOrleansScheduler,
            IKasBuilder kasBuilder,
            ISchemeBuilder schemeBuilder,
            ISecretKeyingMaterialBuilder serverSecretKeyingMaterialBuilder,
            ISecretKeyingMaterialBuilder iutSecretKeyingMaterialBuilder,
            IEntropyProvider entropyProvider,
            IRandom800_90 random,
            IDsaEccFactory dsaEccFactory,
            IDsaFfcFactory dsaFfcFactory,
            IShaFactory shaFactory
        ) : base(nonOrleansScheduler)
        {
            _kasBuilder = kasBuilder;
            _schemeBuilder = schemeBuilder;
            _serverSecretKeyingMaterialBuilder = serverSecretKeyingMaterialBuilder;
            _iutSecretKeyingMaterialBuilder = iutSecretKeyingMaterialBuilder;
            _entropyProvider = entropyProvider;
            _random = random;
            _dsaEccFactory = dsaEccFactory;
            _dsaFfcFactory = dsaFfcFactory;
            _shaFactory = shaFactory;
        }

        public async Task<bool> BeginWorkAsync(KasSscValParameters param)
        {
            _param = param;

            await BeginGrainWorkAsync();
            return await Task.FromResult(true);
        }

        protected override async Task DoWorkAsync()
        {
            try
            {
                var testPassed = true;

                switch (_param.KasAlgorithm)
                {
                    case KasAlgorithm.Ecc:
                        KeyingMaterialHelpers.SetSecretKeyingMaterialBuilderInformation(
                            _serverSecretKeyingMaterialBuilder, _param.ServerGenerationRequirements,
                            (EccDomainParameters)_param.DomainParameters,
                            _param.ServerEphemeralKey, _param.ServerStaticKey,
                            _entropyProvider,
                            null);

                        KeyingMaterialHelpers.SetSecretKeyingMaterialBuilderInformation(
                            _iutSecretKeyingMaterialBuilder, _param.IutGenerationRequirements,
                            (EccDomainParameters)_param.DomainParameters,
                            _param.IutEphemeralKey, _param.IutStaticKey,
                            _entropyProvider, null);
                        break;
                    case KasAlgorithm.Ffc:
                        KeyingMaterialHelpers.SetSecretKeyingMaterialBuilderInformation(
                            _serverSecretKeyingMaterialBuilder, _param.ServerGenerationRequirements,
                            (FfcDomainParameters)_param.DomainParameters,
                            _param.ServerEphemeralKey, _param.ServerStaticKey,
                            _entropyProvider,
                            null);
                        KeyingMaterialHelpers.SetSecretKeyingMaterialBuilderInformation(
                            _iutSecretKeyingMaterialBuilder, _param.IutGenerationRequirements,
                            (FfcDomainParameters)_param.DomainParameters,
                            _param.IutEphemeralKey, _param.IutStaticKey,
                            _entropyProvider, null);
                        break;
                }

                var serverSecretKeyingMaterial = _serverSecretKeyingMaterialBuilder.Build(_param.KasScheme,
                    _param.ServerGenerationRequirements.KasMode,
                    _param.ServerGenerationRequirements.ThisPartyKasRole,
                    _param.ServerGenerationRequirements.ThisPartyKeyConfirmationRole,
                    _param.ServerGenerationRequirements.KeyConfirmationDirection);
                var iutSecretKeyingMaterial = _iutSecretKeyingMaterialBuilder.Build(_param.KasScheme,
                    _param.IutGenerationRequirements.KasMode, _param.IutGenerationRequirements.ThisPartyKasRole,
                    _param.IutGenerationRequirements.ThisPartyKeyConfirmationRole,
                    _param.IutGenerationRequirements.KeyConfirmationDirection);

                // Set up scheme for IUT
                _schemeBuilder.WithThisPartyKeyingMaterial(iutSecretKeyingMaterial)
                    .WithSchemeParameters(new SchemeParameters(
                        new KasAlgoAttributes(_param.KasScheme),
                        _param.IutGenerationRequirements.ThisPartyKasRole,
                        _param.IutGenerationRequirements.KasMode,
                        _param.IutGenerationRequirements.ThisPartyKeyConfirmationRole,
                        _param.IutGenerationRequirements.KeyConfirmationDirection,
                        KasAssurance.None,
                        null));

                var kasIut = _kasBuilder.WithSchemeBuilder(_schemeBuilder)
                    .Build();

                var result = kasIut.ComputeResult(serverSecretKeyingMaterial);
                var response = new KasSscValResult()
                {
                    Disposition = _param.Disposition,
                    SharedSecretComputationResult = result,
                    TestPassed = testPassed,
                    IutSecretKeyingMaterial = iutSecretKeyingMaterial,
                    ServerSecretKeyingMaterial = serverSecretKeyingMaterial
                };

                // Post run failure checks/setup
                switch (_param.Disposition)
                {
                    case KasSscTestCaseExpectation.FailChangedZ:
                        {
                            var zByteLen = result.Z.BitLength.CeilingDivide(BitString.BITSINBYTE);
                            var z = result.Z.GetDeepCopy();

                            // Modify a random byte in z
                            z[_random.GetRandomInt(0, zByteLen)] += 2;

                            response.SharedSecretComputationResult = new KeyAgreementResult(
                                result.SecretKeyingMaterialPartyU,
                                result.SecretKeyingMaterialPartyV,
                                z);
                            response.TestPassed = false;
                            break;
                        }
                    // check for successful conditions w/ constraints.
                    case KasSscTestCaseExpectation.PassLeadingZeroNibble when result.Z[0] >= 0x10:
                        // generate again, until getting to a zero nibble
                        throw new InitialValuesInvalidException();
                }

                // Some implementations can't output the computed Z in the clear, hash it and add that to the returned result 
                if (_param.HashFunctionZ != HashFunctions.None)
                {
                    var hashFunction = ShaAttributes.GetHashFunctionFromEnum(_param.HashFunctionZ);
                    var sha = _shaFactory.GetShaInstance(hashFunction);
                    var hashZ = sha.HashMessage(response.SharedSecretComputationResult.Z).Digest;

                    response.SharedSecretComputationResult = new KeyAgreementResult(
                        response.SharedSecretComputationResult.SecretKeyingMaterialPartyU,
                        response.SharedSecretComputationResult.SecretKeyingMaterialPartyV,
                        response.SharedSecretComputationResult.Z,
                        hashZ);
                }

                await Notify(response);
            }
            catch (Exception e)
            {
                await Throw(e);
            }
        }
    }
}
