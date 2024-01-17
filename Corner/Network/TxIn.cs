using Corner.Network.Interfaces;

namespace Corner.Network
{
    public class TxIn:ISignable
    {
        public TxOut Output { get; set; }
        public string Sign { get; set; }
    }
}