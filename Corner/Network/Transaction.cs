using Corner.Network.Cryptography.Interfaces;
using Corner.Network.Interfaces;
using System.Security.Cryptography;


namespace Corner.Network
{
    public sealed class Transaction:IHashable
    {
        private int _size;
        private string _hash = null;

        public List<TxIn> Inputs { get; set; }
        public List<TxOut> Outputs { get; set; }

        public byte Version { get; set; }
        public string LockTime { get; set; }



        private const int HeaderSize =
           sizeof(byte) +  //Version
           sizeof(uint);  //Nonce


        public string Hash
        {
            get
            {
                _hash ??= this.CalculateHash(Inputs, Outputs,Version,LockTime);
                return _hash;
            }
        }

        public int Size
        {
            get
            {
                if(_size == 0)
                {
                    _size = HeaderSize; 
                }
                return _size;
            }
        }
    }
}
