using Corner.Network.Consensus.Interfaces;
using Corner.Network.Interfaces;

namespace Corner.Network.Consensus
{
    public class ProofOfWorkConsensus<TData>:IConsensus<TData> where TData: IBlockchainData
    {

        public string Mine(Block<TData> block)
        {
            //TODO: Change difficulty
            int difficulty = (int)block.Header.Height;
            string prefix = new string('0',difficulty);
            uint nonce = 0;

            while(true)
            {
                block.Header.Nonce = nonce;
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
            string prefix = new string('0',(int)block.Header.Height);
            string hash = block.Hash;
            return hash.StartsWith(prefix);
        }


    }
}
