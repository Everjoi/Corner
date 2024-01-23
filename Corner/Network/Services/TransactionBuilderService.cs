using Corner.Cryptography.Interfaces;
using Corner.Network.Interfaces.Rules;


namespace Corner.Network.Services
{
    public class TransactionBuilderService
    {
        private const int TransactionVersion = 0;
        //private readonly IEncryptor _encryptor;
        private readonly IRule[] _rules;
        private const decimal FeeTax = 0.001M;

        public TransactionBuilderService(IEncryptor encryptor,IRule[] rules)
        {
            //_encryptor = encryptor;
            _rules = rules;
        }






        public Transaction Build(List<TxIn> inputs,List<TxOut> outputs)
        {
            var fees = CalculateFees(inputs);

            var transaction = new Transaction()
            {
                Version = TransactionVersion,
                Inputs = inputs,
                Outputs = outputs,
                LockTime = DateTime.UtcNow,
                Fees = fees,
            };

            foreach(var rule in _rules)
            {
                if(!rule.Validate(transaction))
                    throw new ApplicationException("does not meet the requirements rule");
            }

            return transaction;
        }

        private decimal CalculateFees(List<TxIn> inputs)
        {
            var inputCount = (decimal)inputs.Count;
            decimal totalInput = (decimal)inputs.Sum(inpt => inpt.Output.Amount);
            decimal fees = (decimal)(inputCount * totalInput * FeeTax);
            return fees;
        }
    }
}
