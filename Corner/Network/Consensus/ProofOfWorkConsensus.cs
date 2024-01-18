using Corner.Network.Consensus.Interfaces;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Consensus
{
    public class ProofOfWorkConsensus<TData> : IConsensus<TData>
    {
 


        public string Mine(Block<TData> block)
        {
            //TODO: Change difficulty
            int difficulty = (int)block._header.Index;
            string prefix = new string('0',difficulty);
            uint nonce = 0;

            while(true)
            {
                block._header.Nonce = nonce;
                string hash = block.Hash;
                if(hash.StartsWith(prefix))
                {
                    Console.WriteLine($"Block mined! Nonce: {nonce}, Hash: {hash}");
                    return hash;
                }

                nonce++;
            }
        }

        public bool Validate(Block<TData> block)
        {
            string prefix = new string('0',(int)block._header.Index);
            string hash = block.Hash;
            return hash.StartsWith(prefix);
        }

 
    }
}
