using System.Security.Cryptography;


namespace Corner.Cryptography.Interfaces
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
