using Corner.Network.Cryptography.Interfaces;

namespace Corner.Network.Interfaces
{
    public interface IBlockchainData : IHashable
    {
        string Id { get; }
        ulong Fees { get; set; }  
    }
}
