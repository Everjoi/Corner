
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Interfaces.Rules
{
    public interface IRule
    {
        bool Validate(Transaction newTransaction);
    }
}
