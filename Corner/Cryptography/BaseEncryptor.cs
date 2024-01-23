using Corner.Cryptography.Interfaces;

namespace Corner.Cryptography
{
    public abstract class BaseEncryptor : IEncryptor
    {

        public abstract KeyPair GenerateKeys();

        public abstract string Sign(string data, string privateKey);

        public abstract bool VerifySign(string publicKey, string data, string sign);

        public abstract string Encrypt(string publicKey, string data);

        public abstract string Decrypt(string privateKey, string data);




        protected static byte[] FillArray(int length, byte[] byteRepresentation, ref int pos)
        {
            var result = new byte[length];
            Array.Copy(byteRepresentation, pos, result, 0, length);
            pos += length;
            return result;
        }

        protected static int GetLength(byte[]? d) => d?.Length ?? 0;

        protected static int CopyTo(byte[] buffer, byte[]? dataToCopy, int pos)
        {
            if (dataToCopy != null)
            {
                dataToCopy.CopyTo(buffer, pos);
                return pos + dataToCopy.Length;
            }
            return pos;
        }


    }
}
