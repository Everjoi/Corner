namespace Corner.Cryptography
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
