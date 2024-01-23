using Corner.Cryptography.Interfaces;

namespace Corner.Network.Interfaces
{
    public interface IBlockchainData : IHashable
    {
        string Id { get; }
        decimal Fees { get; set; }  
    }
}
