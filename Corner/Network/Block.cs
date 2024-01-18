using Corner.Network.Cryptography.Interfaces;
using System.Text;

namespace Corner.Network
{
    public sealed class Block<T>:IHashable
    {
        public Header _header;
        public List<T> Data { get; set; }
        private string _hash;

        public string Hash
        {
            get
            {
                return this.CalculateHash(Data,_header.PrevHash,Nonce);
            }
        }

        // The hash of the previous block.
        public string PrevHash => _header.PrevHash;

        // The merkle root of the transactions.
        public string MerkleRoot => _header.MerkleRoot;

        // The version of the block.
        public uint Version => _header.Version;

        //The timestamp of the block.
        public string Timestamp => _header.Timestamp;

        //The random number of the block.
        public ulong Nonce => _header.Nonce;

        // The index of the block.
        public uint Index => _header.Index;

        // The primary index of the consensus node that generated this block.
        public byte PrimaryIndex => _header.PrimaryIndex;

        // The multi-signature address of the consensus nodes that generates the next block.
        public string NextConsensus => _header.NextConsensus;

        // Size of block 
        public int Size => _header.Size + Encoding.UTF8.GetByteCount(Helper.Serialize(Data));

    }
}
