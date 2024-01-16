using Corner.Network.Interfaces;
using Corner.Network.Interfaces.Rules;
using Corner.Network.Services;
using Corner.Network.Services.Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using static System.Reflection.Metadata.BlobBuilder;


namespace Corner.Network
{
    public sealed class Blockchain<TData> : IBlockchain<TData>  where TData : new()
    {
        private readonly HeaderBuilderService<TData> _hederBuilder; // build header 
        private  Block<TData> _lastBlock; // last block in blockchain 
        private static List<TData> _dataList;
        public readonly List<Block<TData>> _blocks; // blocks in blockchain 
        // private readonly Consensus _consensus; // pow pos 

        public Blockchain()
        {
            _dataList = new List<TData>();
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
            //var _block = BuildBlock(block.Data);
            AddBlock(block);
            _lastBlock = block;
        }

         
        private void AddGenesisBlock()
        {
            var genesisBlock = new Block<TData>
            {
                _header = _hederBuilder.BuildBlockHeader(),
                Data =  new List<TData> {  new TData() }
            };

            _blocks.Add(genesisBlock);
        }


        public void PerformAction(TData data)
        {
            int totalSize = _dataList.Sum(payload => GetObjectSize(payload));      
            if(totalSize >= 1024 * 1024) // 1 MB  
            {
                var newBlock = BuildBlock(_dataList);
                AcceptBlock(newBlock);
                _dataList = new List<TData>(); 
            }
            _dataList.Add(data);
        }


        private int GetObjectSize(TData data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);
            return jsonData.Length;
        }

    }
}
