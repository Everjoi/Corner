using Corner.Cryptography.Interfaces;
using Corner.Network.Interfaces;
using NBitcoin.Protocol;
using System.Text;

namespace Corner.Network
{
    public sealed class Block<T>:IHashable where T : IBlockchainData
    {
        private Header _header;
        private string _hash;
        private int _transactionCount;
        private decimal _totalFees;
        private List<string> _dataIds;

        public List<T> Data { get; set; }

        public Header Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        public string Hash
        {
            get
            {
                _hash = this.CalculateHash(Data,_header.PrevHash,Nonce);
                return _hash;
            }
        }

        public string PrevHash => _header.PrevHash;
        public string MerkleRoot => _header.MerkleRoot;
        public uint Version => _header.Version;
        public string Timestamp => _header.Timestamp;
        public ulong Nonce => _header.Nonce;
        public uint Index => _header.Height;
        public string NextConsensus => _header.NextConsensus;

        public int Size => _header.Size + Encoding.UTF8.GetByteCount(Helper.Serialize(Data));

        public int TransactionCount
        {
            get 
            {
                _transactionCount = Data.Count;
                return _transactionCount;
            }
        }

        public decimal TotalFees
        {
            get 
            {
                //_totalFees = Data.Aggregate(0UL,(sum,data) => sum + data.Fees);
                _totalFees = Data.Sum(x => x.Fees);
                return _totalFees;
            }
        }

        public List<string> DataIds
        {
            get
            {
                _dataIds = Data.Select(data => data.Id).ToList();
                return _dataIds;
            }
        }

        public int MiningTimeSeconds { get; set; }

    }
}
