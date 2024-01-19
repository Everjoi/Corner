using Corner.Network.Consensus.Interfaces;
using Corner.Network.Interfaces;
using System.Diagnostics;

namespace Corner.Network.Consensus
{
    public class ProofOfWorkConsensus<TData>:IConsensus<TData> where TData: IBlockchainData
    {
        private const int DesiredAverageMiningTimeSeconds = 100;
        public IBlockchain<TData> _blockchain;
        private int _difficulty;

        public ProofOfWorkConsensus()
        {
            _blockchain = null;
            _difficulty = 0;
        }



        public string Mine(Block<TData> block)
        {
            Stopwatch stopwatch = new Stopwatch();
            _difficulty = GetDifficulty();
            string prefix = new string('0',_difficulty);
            uint nonce = 0;
            stopwatch.Start();
            while(true)
            {
                block.Header.Nonce = nonce;
                string hash = block.Hash;
                if(hash.StartsWith(prefix))
                {
                    stopwatch.Stop();
                    block.MiningTimeSeconds = (int)stopwatch.Elapsed.TotalSeconds;
                    Console.WriteLine($"Block Mined!: Nonse: {nonce} Hash: {hash}");
                    Console.WriteLine($"Mining Time: {block.MiningTimeSeconds}");
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


        private int GetDifficulty()
        {
            int currentAverageMiningTimeSeconds = GetAverageMiningTime();  
            if(currentAverageMiningTimeSeconds > DesiredAverageMiningTimeSeconds)
            {
                return _difficulty-1;  
            }
            else
            {
                return _difficulty+1;  
            }
        }


        private int GetAverageMiningTime()
        {
            int totalSecond = 0;

            foreach(var block in _blockchain.Blocks)
            {
                totalSecond += block.MiningTimeSeconds;
            }

            return totalSecond / _blockchain.Blocks.Count;

        }


    }
}
