using System.Text;

namespace Corner.Network
{
    public sealed class Header
    {
        private uint _version;
        private string _prevHash;
        private string _merkleRoot;
        private string _timestamp;
        private ulong _nonce;
        private uint _height;
        private string _nextConsensus;


        public uint Version
        {
            get => _version;
            set { _version = value; }
        }


        public string PrevHash
        {
            get => _prevHash;
            set { _prevHash = value; }

        }


        public string MerkleRoot
        {
            get => _merkleRoot;
            set { _merkleRoot = value; }
        }


        public string Timestamp
        {
            get => _timestamp;
            set { _timestamp = value; }
        }

        public ulong Nonce
        {
            get => _nonce;
            set { _nonce = value; }
        }


        public uint Height
        {
            get => _height;
            set { _height = value; }
        }



        public string NextConsensus
        {
            get => _nextConsensus;
            set { _nextConsensus = value; }
        }





        public int Size =>
           sizeof(uint) +      // Version
           Encoding.UTF8.GetByteCount(_prevHash) +    // PrevHash    
           Encoding.UTF8.GetByteCount(_merkleRoot) +    // MerkleRoot   
           sizeof(ulong) +     // Timestamp
           sizeof(ulong) +      // Nonce
           sizeof(uint) +      // Index
           sizeof(byte) +      // PrimaryIndex
           Encoding.UTF8.GetByteCount(_nextConsensus);  // NextConsensus   

    }
}
