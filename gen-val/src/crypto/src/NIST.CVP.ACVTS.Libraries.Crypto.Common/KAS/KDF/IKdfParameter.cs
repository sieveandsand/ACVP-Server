﻿using Newtonsoft.Json;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.Enums;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Crypto.Common.KAS.KDF
{
    /// <summary>
    /// Represents information needed for invoking a KDF function
    /// </summary>
    public interface IKdfParameter
    {
        /// <summary>
        /// The type of KDF supported.
        /// </summary>
        Kda KdfType { get; }
        /// <summary>
        /// Some KDFs require a second set of nonces outside the generation scope of the KAS scheme.
        /// This flag is used to indicate when an additional pair of nonces is required.
        /// </summary>
        /// <remarks>This flag should be true for all SP800-135 KDFs, and false for the SP800-56C KDFs.</remarks>
        bool RequiresAdditionalNoncePair { get; }
        /// <summary>
        /// The Salt used with MAC based KDFs.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString Salt { get; set; }
        /// <summary>
        /// The IV used with some KDFs.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString Iv { get; set; }
        /// <summary>
        /// Additional derived secret to be included as a part of fixed info
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString T { get; set; }
        /// <summary>
        /// The shared secret for use in deriving a key.
        /// </summary>
        BitString Z { get; set; }
        /// <summary>
        /// The length of the key to derive.
        /// </summary>
        int L { get; set; }
        /// <summary>
        /// The pattern to use when constructing fixed info.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        string FixedInfoPattern { get; set; }
        /// <summary>
        /// The encoding type of the fixedInput
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        FixedInfoEncoding FixedInputEncoding { get; set; }
        /// <summary>
        /// The algorithm ID indicator.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString AlgorithmId { get; set; }
        /// <summary>
        /// The Label for the transaction.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString Label { get; set; }
        /// <summary>
        /// The Context for the transaction.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString Context { get; set; }
        /// <summary>
        /// Additional nonce used by the initiator for some KDFs.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString AdditionalInitiatorNonce { get; set; }
        /// <summary>
        /// Additional nonce used by the responder for some KDFs.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString AdditionalResponderNonce { get; set; }
        /// <summary>
        /// Additional EntropyBits as a part of fixed info pattern
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        BitString EntropyBits { get; set; }
        /// <summary>
        /// Accepts a <see cref="IKdfVisitor"/> this will in turn dispatch a call to a supported KDF.
        /// </summary>
        /// <param name="visitor">Describes how to invoke a KDF for implementors.</param>
        /// <param name="fixedInfo">The contextual fixed information to be plugged into a kdf.</param>
        /// <returns>A derived key.</returns>
        KdfResult AcceptKdf(IKdfVisitor visitor, BitString fixedInfo);
        /// <summary>
        /// Set the C or Nonce values for the initiator and responder, for use in SP800-135 KDFs.
        /// </summary>
        /// <param name="initiatorData">The initiator ephemeral data (either a C value or nonce).</param>
        /// <param name="responderData">The responder ephemeral data (either a C value or nonce).</param>
        void SetEphemeralData(BitString initiatorData, BitString responderData);
    }
}
