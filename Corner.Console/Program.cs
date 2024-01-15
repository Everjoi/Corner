using Corner.Network;
using Corner.Network.Cryptography;
using Corner.Network.Services;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
 


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
            var transactions = new List<Transaction>();
            var transactionBuilder = new TransactionBuilderService(encrypthor,rules,newBlock);


            // create 100 coin for user1 and user2

            var createCoin = new Transaction()
            {
                Outputs = 
                {
                    new TxOut(){Adress = user1.PublicKey, Amount = 100},
                    new TxOut(){Adress = user2.PublicKey, Amount = 100}
                }
            };
            newBlock.Data = new List<Transaction>() { createCoin };
            blockchain.AddBlock(newBlock);



            
            for(int i = 0; i < 5; i++)
            {
                var transaction = new Transaction()
                {
                    Inputs =
                    {
                        new TxIn(){Amount = 2, PrevHash = null}
                    },
                    Outputs =
                    {
                        new TxOut() { Adress = user2.PublicKey, Amount = 2}
                    },

                };

                var txOut = new TxOut();
                txOut.Adress = user2.PublicKey;
                txOut.Amount = 2;

                var txIn = new TxIn();
                txIn.Amount = txOut.Amount;

                var tx = transactionBuilder.Build(new List<TxIn> { }, new List<TxOut> { });
                transactionBuilder.Sign(tx,user1.SecretKey);
                transactions.Add(tx);
            }


            // perform block in blockchain 
            var block = new Block<Transaction>();
            blockchain.BuildBlock(transactions);
           

        }
    }
}