using Corner.Network.Cryptography.Interfaces;
using Corner.Network.Interfaces.Rules;
using System.Text.Json;


namespace Corner.Network.Services
{
    public class TransactionBuilderService
    {
        private const int TransactionVersion = 0;
        private readonly IEncryptor _encryptor;
        private readonly IRule[] _rules;


        public TransactionBuilderService(IEncryptor encryptor,IRule[] rules)
        {
            _encryptor = encryptor;
            _rules = rules; // GetAllRules() from reflection 
        }


        public string Sign(TxIn transaction,string privateKey)
        {
            var dataRaw = JsonSerializer.Serialize(transaction);
            return _encryptor.Sign(dataRaw,privateKey);
        }

        public bool IsValid(string publicKey,TxIn transaction,string sign)
        {
            var data = JsonSerializer.Serialize(transaction);
            return IsValid(publicKey,data,sign);
        }

        public bool IsValid(string publicKey,string data,string sign)
        {
            return _encryptor.VerifySign(publicKey,data,sign);
        }


        public Transaction Build(List<TxIn> inputs,List<TxOut> outputs)
        {

            var transaction = new Transaction()
            {
                Version = TransactionVersion,
                Inputs = inputs,
                Outputs = outputs,
                LockTime = DateTime.UtcNow,
            };

            foreach(var rule in _rules)
            {
                if(!rule.Validate(transaction))
                    throw new ApplicationException("does not meet the requirements rule");
            }

            return transaction;
        }


    }
}
