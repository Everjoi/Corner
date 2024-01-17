namespace Corner.Network.Interfaces
{
    public interface IBlockchain<TData>
    {
        void AddBlock(Block<TData> block);
        Block<TData> BuildBlock(List<TData> data);
        void AcceptBlock(Block<TData> typedBlock);
    }
}
