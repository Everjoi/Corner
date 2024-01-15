using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Cryptography
{
    public class KeyPair
    {

        public KeyPair(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            SecretKey = privateKey;
        }

        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
