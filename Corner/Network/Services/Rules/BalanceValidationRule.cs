using Corner.Network.Interfaces.Rules;



namespace Corner.Network.Services.Rules
{
    public class BalanceValidationRule : IRule
    {

        public bool Validate(Transaction newTransaction)
        {
            decimal totalOutputs = 0;
            foreach(var output in newTransaction.Outputs)
            {
                totalOutputs += output.Amount;
            }

            decimal totalInputs = 0;
            foreach(var input in newTransaction.Inputs)
            {
                totalInputs += input.Output.Amount;
            }

            
            return totalOutputs <= totalInputs;
        }
    }
}
