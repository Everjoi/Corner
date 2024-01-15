using Corner.Network.Interfaces;

namespace Corner.Network
{
    public class TxIn : ISignable
    {
        public string TxOutId { get; set; }
        public uint TxOutIndex { get; set; }
        public string Sign { get; set; }   
    }
}