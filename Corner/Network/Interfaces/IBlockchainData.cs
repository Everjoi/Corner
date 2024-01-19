namespace Corner.Network.Interfaces
{
    public interface IBlockchainData
    {
        string Id { get; }
        ulong Fees { get; set; }  
    }
}
