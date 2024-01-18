

using NBitcoin.RPC;

namespace Corner.Network.Services
{
    public class HeaderBuilderService<TData>
    {
        private static uint _index;
        private const uint Version = 1;
        public  Block<TData> _prevBlock;

        public HeaderBuilderService(Block<TData> prevBlock)
        {
            _index = 0;
            _prevBlock = prevBlock;
        }

        public Header BuildBlockHeader()
        {
            var blockHeader = new Header()
            {
                Nonce = 0,
                Index = _index,
                MerkleRoot = CreateMerkleRoot(),
                Version = Version,
                Timestamp = DateTime.Now.ToString(),
                NextConsensus = "PoW",
                PrevHash = _prevBlock.Hash
            }; 
            _index++;
            return blockHeader;
        }


        private string CreateMerkleRoot()
        {
            return string.Empty;
        }

    }
}
