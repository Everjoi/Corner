using Corner.Network.Consensus.Interfaces;
using Corner.Network.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Consensus
{
    public class ProfOfStake<TData>:IConsensus<TData> where TData : IBlockchainData
    {

        public ProfOfStake()
        {
            
        }

        public string Mine(Block<TData> block)
        {
            throw new NotImplementedException();
        }

        public bool Validate(Block<TData> block)
        {
            throw new NotImplementedException();
        }
    }
}
