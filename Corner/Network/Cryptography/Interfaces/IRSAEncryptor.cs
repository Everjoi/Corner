using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Cryptography.Interfaces
{
    public interface IRSAEncryptor : IEncryptor
    {
        byte[] GetArray(RSAParameters p);
        RSAParameters GetPrivateKey(byte[] byteRepresentation);
        RSAParameters GetPublicKey(byte[] byteRepresentation);
        RSACryptoServiceProvider GetCryptoProvider(string privateKey);
        RSACryptoServiceProvider GetVerificationProvider(string publicKey);

    }
}
