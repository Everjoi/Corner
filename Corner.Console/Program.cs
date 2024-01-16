using Corner.Network;
using Corner.Network.Cryptography;
using Corner.Network.Services;
using Corner.Network.Interfaces.Rules;
using Corner.Network.Services.Rules;


namespace Corner.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1 - setup blockchain
            var encrypthor = new RSAEncryptor();
            var blockchain = new Blockchain<Transaction>();

            // create user (wallet)
            var user1 = encrypthor.GenerateKeys();
            var user2 = encrypthor.GenerateKeys();

            // user1 do transation to user2
            var send = new TxOut() { Adress = user2.PublicKey,Amount = 10 };

            IRule[] rules = { new BalanceValidationRule(),new SignValidationRule(encrypthor,blockchain._blocks) };
            var transactionBuilder = new TransactionBuilderService(encrypthor,rules);
           
            var transaction = transactionBuilder.Build(null,new List<TxOut> { send });

           


            blockchain.PerformAction(transaction);
        }
    }
}