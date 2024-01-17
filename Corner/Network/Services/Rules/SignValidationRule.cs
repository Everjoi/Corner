using Corner.Network.Cryptography;
using Corner.Network.Cryptography.Interfaces;
using Corner.Network.Interfaces;
using Corner.Network.Interfaces.Rules;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Services.Rules
{
    public class SignValidationRule:IRule
    {
        private IEncryptor _encryptor;
        private readonly List<Block<Transaction>> _blockchain;

        public SignValidationRule(IEncryptor encryptor, List<Block<Transaction>> blockchain)
        {
            _encryptor = encryptor;
            _blockchain = blockchain;
        }

        public bool Validate(Transaction newTransaction)
        {

            foreach(var input in newTransaction.Inputs)
            {
                string inputData = $"{input.Output.Adress}:{input.Output.Amount}";
                bool isSignatureValid = _encryptor.VerifySign(input.Output.Adress,inputData,input.Sign);

                if(!isSignatureValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
