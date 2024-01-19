using Corner.Network.Consensus.Interfaces;
using Corner.Network.Interfaces;
using Corner.Network.Services;
using System.Text;
using System.Text.Json;
//using static System.Reflection.Metadata.BlobBuilder;


namespace Corner.Network
{
    public sealed class Blockchain<TData>:IBlockchain<TData> where TData : IBlockchainData, new()
    {
        private readonly HeaderBuilderService<TData> _hederBuilder; // build header 
        private Block<TData> _lastBlock; // last block in blockchain 
        private static List<TData> _dataList;
        private readonly List<Block<TData>> _blocks; // blocks in blockchain 
        private readonly IConsensus<TData> _consensus; // pow pos 

        public Blockchain(IConsensus<TData> consensus)
        {
            _consensus = consensus;
            _dataList = new List<TData>();
            _blocks = new List<Block<TData>>();
            var genesis = AddGenesisBlock();
            _lastBlock = genesis;
            _hederBuilder = new HeaderBuilderService<TData>(genesis);
        }



        public List<Block<TData>> Blocks {
            get
            {
                return _blocks;
            }
        }

        public void AddBlock(Block<TData> block)
        {
            if(block.PrevHash == _lastBlock.Hash)
            {
                var expectHash = block.CalculateHash(block.Data,block.PrevHash,block.Nonce);
                if(block.Hash == expectHash)
                    _blocks.Add(block);
                else
                    throw new ArgumentException($"Hashes do not math. Expect hash {expectHash}; current hash {block.Hash} ");
            }
            else
                throw new ArgumentException($"New block dos not following by last block");

        }



        public Block<TData> BuildBlock(List<TData> data) // transactions 
        {
            var block = new Block<TData>()
            {
                Header = _hederBuilder.BuildBlockHeader(),
                Data = data
            };
            _hederBuilder._prevBlock = block;
            // hash - PoW
            var hash = _consensus.Mine(block);

            return block;
        }


        public void AcceptBlock(Block<TData> block)
        {
 
            if(_consensus.Validate(block))
                AddBlock(block);
            else
                throw new ApplicationException("pow hash is not valid");
            _lastBlock = block;
        }


        private Block<TData> AddGenesisBlock()
        {
            var genesisBlock = new Block<TData>
            {
                Header = new Header()
                {
                    Timestamp = DateTime.Now.ToString(),
                    PrevHash = "",
                    Nonce = 0,
                },
                Data = new List<TData> { new TData() },
            };
            _blocks.Add(genesisBlock);
            return genesisBlock;
        }


        public void PerformAction(TData data)
        {
            int totalSize = _dataList.Sum(payload => GetObjectSize(payload));
            if(totalSize >= 1024 * 1024)  
            {
                var newBlock = BuildBlock(_dataList);
                AcceptBlock(newBlock);
                _dataList = new List<TData>();
            }
            _dataList.Add(data);
        }


        private int GetObjectSize(TData data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);
            return jsonData.Length;
        }

    }
}
