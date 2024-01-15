using Corner.Network.Cryptography.Interfaces;
using Corner.Network.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corner.Network.Services
{
    public class TransactionBuilderService 
    {
        private const int TransactionVersion = 0;
        private readonly IEncryptor _encryptor;
        private readonly IRule[] _rules;
        private Block<Transaction> _block;
        private Blockchain<Transaction> _blockchain;

        public TransactionBuilderService(IEncryptor encryptor,IRule[] rules, Block<Transaction> block)
        {
            _encryptor = encryptor;
            _rules = rules; // GetAllRules() from reflection 
            _block = block;
        }


        public string Sign(Transaction transaction, string privateKey)
        {
            var dataRaw = JsonSerializer.Serialize(transaction);

            foreach(var input in transaction.Inputs)
            {
                input.Sign = _encryptor.Sign( dataRaw, privateKey);
            }      

            return _encryptor.Sign(dataRaw, privateKey);
        }

        public bool IsValid(string publicKey, Transaction transaction, string sign)
        {
            var data = JsonSerializer.Serialize(transaction);
            return IsValid(publicKey,data,sign);
        }

        public bool IsValid(string publicKey, string data, string sign)
        {
            return _encryptor.VerifySign(publicKey,data,sign);
        }


        public Transaction Build(List<TxIn> inputs, List<TxOut> outputs)
        {
            var transaction = new Transaction()
            {
                Version = TransactionVersion,
                Inputs = inputs,
                Outputs = outputs,
            };

            return transaction;
        }




    }
}
