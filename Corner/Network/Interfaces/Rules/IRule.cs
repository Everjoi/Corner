namespace Corner.Network.Interfaces.Rules
{
    public interface IRule
    {
        bool Validate(Transaction newTransaction);
    }
}
