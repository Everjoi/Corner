using Corner.Network.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Consensus.Interfaces
{
    public interface IConsensus<TData> where TData : IBlockchainData
    {
        string Mine(Block<TData> block);
        bool Validate(Block<TData> block);
    }
}
