using Corner.Cryptography;

namespace Corner.Cryptography.Interfaces
{
    public interface IEncryptor
    {
        KeyPair GenerateKeys();
        string Sign(string data, string privateKey);
        bool VerifySign(string publicKey, string data, string sign);

        string Encrypt(string publicKey, string data);
        string Decrypt(string privateKey, string data);
    }
}
