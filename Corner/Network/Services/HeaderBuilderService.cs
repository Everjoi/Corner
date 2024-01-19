

using Corner.Network.Interfaces;
using NBitcoin.RPC;

namespace Corner.Network.Services
{
    public class HeaderBuilderService<TData> where TData : IBlockchainData
    {
        private static uint _height;
        private const uint Version = 1;
        public  Block<TData> _prevBlock;

        public HeaderBuilderService(Block<TData> prevBlock)
        {
            _height = 0;
            _prevBlock = prevBlock;
        }

        public Header BuildBlockHeader()
        {
            var blockHeader = new Header()
            {
                Nonce = 0,
                Height = _height,
                MerkleRoot = CreateMerkleRoot(),
                Version = Version,
                Timestamp = DateTime.Now.ToString(),
                NextConsensus = "PoW",
                PrevHash = _prevBlock.Hash
            }; 
            _height++;
            return blockHeader;
        }


        private string CreateMerkleRoot()
        {
            return string.Empty;
        }

    }
}
