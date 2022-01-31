﻿using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ParameterTypes;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.ResultTypes;
using Orleans;

namespace NIST.CVP.ACVTS.Libraries.Orleans.Grains.Interfaces.Hash
{
    public interface IOracleObserverTupleHashCaseGrain : IGrainWithGuidKey, IGrainObservable<TupleHashResult>
    {
        Task<bool> BeginWorkAsync(TupleHashParameters param);
    }
}
