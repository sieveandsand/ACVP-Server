﻿using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Common;
using NIST.CVP.ACVTS.Libraries.Math.Entropy;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Ctr;
using NIST.CVP.ACVTS.Libraries.Orleans.Grains.Interfaces.Tdes;

namespace NIST.CVP.ACVTS.Libraries.Orleans.Grains.Tdes
{
    public class OracleObserverTdesDeferredCounterCaseGrain : ObservableOracleGrainBase<TdesResult>,
        IOracleObserverTdesDeferredCounterCaseGrain
    {
        private readonly IEntropyProvider _entropyProvider;

        private CounterParameters<TdesParameters> _param;

        public OracleObserverTdesDeferredCounterCaseGrain(
            LimitedConcurrencyLevelTaskScheduler nonOrleansScheduler,
            IEntropyProviderFactory entropyProviderFactory
        ) : base(nonOrleansScheduler)
        {
            _entropyProvider = entropyProviderFactory.GetEntropyProvider(EntropyProviderTypes.Random);
        }

        public async Task<bool> BeginWorkAsync(CounterParameters<TdesParameters> param)
        {
            _param = param;

            await BeginGrainWorkAsync();
            return await Task.FromResult(true);
        }

        protected override async Task DoWorkAsync()
        {
            await Notify(CounterHelpers.GetDeferredCounterTest(_param, _entropyProvider));
        }
    }
}
