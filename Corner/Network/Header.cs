using Corner.Network.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Corner.Network
{
    public sealed class Header  
    {
        private uint version;
        private string prevHash; 
        private string merkleRoot; 
        private ulong timestamp;
        private ulong nonce;
        private uint index;
        private byte primaryIndex;
        private string nextConsensus;  

 

        public uint Version
        {
            get => version;
            set { version = value; }
        }


        public string PrevHash
        {
            get => prevHash;
            set { prevHash = value; }

        }


        public string MerkleRoot
        {
            get => merkleRoot;
            set { merkleRoot = value; }
        }


        public ulong Timestamp
        {
            get => timestamp;
            set { timestamp = value;}
        }

        public ulong Nonce
        {
            get => nonce;
            set { nonce = value; }
        }


        public uint Index
        {
            get => index;
            set { index = value; }
        }


        public byte PrimaryIndex
        {
            get => primaryIndex;
            set { primaryIndex = value; }
        }


        public string NextConsensus
        {
            get => nextConsensus;
            set { nextConsensus = value; }
        }


        


        public int Size =>
           sizeof(uint) +      // Version
           Encoding.UTF8.GetByteCount(prevHash) +    // PrevHash    
           Encoding.UTF8.GetByteCount(merkleRoot) +    // MerkleRoot   
           sizeof(ulong) +     // Timestamp
           sizeof(ulong) +      // Nonce
           sizeof(uint) +      // Index
           sizeof(byte) +      // PrimaryIndex
           Encoding.UTF8.GetByteCount(nextConsensus);  // NextConsensus   
               
    }
}
