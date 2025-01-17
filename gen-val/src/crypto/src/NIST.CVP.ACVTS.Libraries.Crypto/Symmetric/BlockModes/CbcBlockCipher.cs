﻿using System;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.BlockModes;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.Engines;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Symmetric.Enums;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Crypto.Symmetric.BlockModes
{
    public class CbcBlockCipher : ModeBlockCipherBase<SymmetricCipherResult>
    {
        public override bool IsPartialBlockAllowed => false;

        public CbcBlockCipher(IBlockCipherEngine engine) : base(engine) { }

        public override SymmetricCipherResult ProcessPayload(IModeBlockCipherParameters param)
        {
            CheckPayloadRequirements(param.Payload);
            var key = param.Key.ToBytes();

            var engineParam = new EngineInitParameters(param.Direction, key, param.UseInverseCipherMode);
            _engine.Init(engineParam);

            var numberOfBlocks = GetNumberOfBlocks(param.Payload.BitLength);
            var outBuffer = GetOutputBuffer(param.Payload.BitLength);

            if (param.Direction == BlockCipherDirections.Encrypt)
            {
                Encrypt(param, numberOfBlocks, outBuffer);
            }
            else
            {
                Decrypt(param, numberOfBlocks, outBuffer);
            }

            var output = new BitString(outBuffer);
            return new SymmetricCipherResult(output.GetMostSignificantBits(param.Payload.BitLength));
        }

        private void Encrypt(IModeBlockCipherParameters param, int numberOfBlocks, byte[] outBuffer)
        {
            var payLoad = param.Payload.GetDeepCopy().ToBytes();
            var iv = param.Iv.GetDeepCopy().ToBytes();

            // For each block
            for (int i = 0; i < numberOfBlocks; i++)
            {
                // XOR IV onto current block payload
                for (int j = 0; j < _engine.BlockSizeBytes; j++)
                {
                    payLoad[i * _engine.BlockSizeBytes + j] ^= iv[j];
                }

                _engine.ProcessSingleBlock(payLoad, outBuffer, i);

                // Update Iv with current block's outBuffer values
                Array.Copy(outBuffer, i * _engine.BlockSizeBytes, iv, 0, _engine.BlockSizeBytes);
            }

            // Update the Param.Iv for the next call
            for (int i = 0; i < _engine.BlockSizeBytes; i++)
            {
                param.Iv[i] = iv[i];
            }
        }

        private void Decrypt(IModeBlockCipherParameters param, int numberOfBlocks, byte[] outBuffer)
        {
            var payLoad = param.Payload.ToBytes();
            var iv = param.Iv.GetDeepCopy().ToBytes();

            // For each block
            for (int i = 0; i < numberOfBlocks; i++)
            {
                _engine.ProcessSingleBlock(payLoad, outBuffer, i);

                // XOR IV onto current block outBuffer
                for (int j = 0; j < _engine.BlockSizeBytes; j++)
                {
                    outBuffer[i * _engine.BlockSizeBytes + j] ^= iv[j];
                }

                // Update Iv with current block's payLoad values
                Array.Copy(payLoad, i * _engine.BlockSizeBytes, iv, 0, _engine.BlockSizeBytes);
            }

            // Update the Param.Iv for the next call
            for (int i = 0; i < _engine.BlockSizeBytes; i++)
            {
                param.Iv[i] = iv[i];
            }
        }
    }
}
