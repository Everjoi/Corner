using Corner.Network;
using Corner.Network.Consensus;
using Corner.Network.Cryptography;
using Corner.Network.Interfaces.Rules;
using Corner.Network.Services;
using Corner.Network.Services.Rules;


namespace Corner.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var complexity = (int)(Math.Log2(blockchain._blocks.Count + 1) + 1);

            // 1 - setup blockchain
            var consensus = new ProofOfWorkConsensus<Transaction>();
            var encrypthor = new RSAEncryptor();
            var blockchain = new Blockchain<Transaction>(consensus);

            // create user (wallet)
            var user1 = encrypthor.GenerateKeys();
            var user2 = encrypthor.GenerateKeys();

            // user1 do transation to user2
            
            var input = new List<TxIn>
            {
                new TxIn
                {
                    Output = new TxOut { Adress = user1.PublicKey, Amount = 10 },
                    Sign = encrypthor.Sign($"{user1.PublicKey}:{10}", user1.SecretKey)
                }
            };


            var output = new List<TxOut>
            {
                // User 2 receives some amount
                new TxOut { Adress = user2.PublicKey, Amount = 5 },
                // User 1 gets back the remaining amount (change)
                new TxOut { Adress = user1.PublicKey, Amount = 5 }
            };

            IRule[] rules = { new BalanceValidationRule(),new SignValidationRule(encrypthor,blockchain.Blocks) };
            var transactionBuilder = new TransactionBuilderService(encrypthor,rules);

            var transaction = transactionBuilder.Build(input,output);


            blockchain.PerformAction(transaction);     
        }
    }
}