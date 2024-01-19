using Corner.Network.Interfaces;

namespace Corner.Network.Consensus.Interfaces
{
    public interface IConsensus<TData> where TData : IBlockchainData
    {
        string Mine(Block<TData> block);
        bool Validate(Block<TData> block);
    }
}
