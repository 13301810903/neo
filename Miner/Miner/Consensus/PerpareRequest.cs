﻿using AntShares.Core;
using AntShares.IO;
using System;
using System.IO;
using System.Linq;

namespace AntShares.Miner.Consensus
{
    internal class PerpareRequest : ConsensusMessage
    {
        public ulong Nonce;
        public UInt256[] TransactionHashes;
        public MinerTransaction MinerTransaction;

        public PerpareRequest()
            : base(ConsensusMessageType.PerpareRequest)
        {
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            Nonce = reader.ReadUInt64();
            TransactionHashes = reader.ReadSerializableArray<UInt256>();
            if (TransactionHashes.Distinct().Count() != TransactionHashes.Length)
                throw new FormatException();
            MinerTransaction = reader.ReadSerializable<MinerTransaction>();
            if (MinerTransaction.Hash != TransactionHashes[0])
                throw new FormatException();
        }

        public override void Serialize(BinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Nonce);
            writer.Write(TransactionHashes);
            writer.Write(MinerTransaction);
        }
    }
}
