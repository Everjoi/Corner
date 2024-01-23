using Corner.Network.Interfaces;


namespace Corner.Network
{
    public sealed class Transaction:IBlockchainData
    {
        private int _size;
        private string _hash = null;
        private string _id;

        public List<TxIn> Inputs { get; set; }
        public List<TxOut> Outputs { get; set; }

        public byte Version { get; set; }
        public DateTime LockTime { get; set; }


        private const int HeaderSize =
           sizeof(byte) +  //Version
           sizeof(uint);  //Nonce


        public string Id { 
            get
            {
                if(_id == null)
                    _id= Guid.NewGuid().ToString();  // TODO: change GUID
                return _id;
            } 
        }

        public string Hash
        {
            get
            {
                _hash ??= this.CalculateHash(Inputs,Outputs,Id);
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

        public decimal Fees { get; set; }

    }
}
