using Corner.Cryptography.Interfaces;
using Corner.Network.Interfaces.Rules;

namespace Corner.Network.Services.Rules
{
    public class SignValidationRule:IRule
    {
        private IEncryptor _encryptor;
        private readonly List<Block<Transaction>> _blockchain;

        public SignValidationRule(IEncryptor encryptor,List<Block<Transaction>> blockchain)
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
