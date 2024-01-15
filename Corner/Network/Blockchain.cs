using Corner.Network.Interfaces;
using Corner.Network.Interfaces.Rules;
using Corner.Network.Services;
using Corner.Network.Services.Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corner.Network
{
    public sealed class Blockchain<TData> : IBlockchain<TData> 
    {
        private readonly HeaderBuilderService<TData> _hederBuilder; // build header 
        private readonly List<Block<TData>> _blocks; // blocks in blockchain 
        private  Block<TData> _lastBlock; // last block in blockchain 
       // private readonly Consensus _consensus; // pow pos 

        public Blockchain()
        {
            _blocks = new List<Block<TData>>();
            _hederBuilder = new HeaderBuilderService<TData>();
            _lastBlock = _blocks.LastOrDefault()!;
            AddGenesisBlock();
        }

        
        public void AddBlock(Block<TData> block)
        {
            if(block.PrevHash == _lastBlock.Hash)
            {
                var expectHash = block.CalculateHash(block.Data, block.PrevHash);
                if(block.Hash == expectHash)
                    _blocks.Add(block);
                else
                    throw new ArgumentException($"Hashes do not math. Expect hash {expectHash}; current hash {block.Hash} ");
            }
            else
                throw new ArgumentException($"New block dos not following by last block");

        }



        public Block<TData> BuildBlock(List<TData> data) // transactions 
        {
            var block = new Block<TData>()
            {
                _header = _hederBuilder.BuildBlockHeader(),
                Data = data
            };

            // hash - PoW
            return block;
        }


        public void AcceptBlock(Block<TData> block)
        {
            // Consensus Pow
            var _block = BuildBlock(block.Data);
            AddBlock(_block);
            _lastBlock = block;
        }


        private void AddGenesisBlock()
        {

        }

    }
}
