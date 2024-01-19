

using Corner.Network.Interfaces;
using NBitcoin.RPC;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Corner.Network.Services
{
    public class HeaderBuilderService<TData> where TData : IBlockchainData
    {
        private static uint _height;
        private const uint Version = 1;
        public  Block<TData> _prevBlock;

        public HeaderBuilderService(Block<TData> prevBlock)
        {
            _height = 0;
            _prevBlock = prevBlock;
        }

        public Header BuildBlockHeader(List<TData> data)
        {
            var blockHeader = new Header()
            {
                Nonce = 0,
                Height = _height,
                MerkleRoot = CreateMerkleRoot(data),
                Version = Version,
                Timestamp = DateTime.Now.ToString(),
                NextConsensus = "PoW",
                PrevHash = _prevBlock.Hash
            }; 
            _height++;
            return blockHeader;
        }


        private string CreateMerkleRoot(List<TData> dataList)
        {

            List<string> merkleTree = new List<string>(dataList.Select(data=>data.Hash));

            while(merkleTree.Count > 1)
            {
                List<string> newTreeLevel = new List<string>();

                for(int i = 0;i < merkleTree.Count;i += 2)
                {
                    string combinedHash = (i + 1 < merkleTree.Count) ?
                                          CombineAndHash(merkleTree[i],merkleTree[i + 1]) :
                                          CombineAndHash(merkleTree[i],merkleTree[i]);

                    newTreeLevel.Add(combinedHash);
                }

                merkleTree = newTreeLevel;
            }

            return merkleTree.FirstOrDefault()!;
        }

        private string CombineAndHash(string hash1,string hash2)
        {
           
            string combined = hash1 + hash2;
            byte[] combinedBytes = Encoding.UTF8.GetBytes(combined);

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
            }
        }


    }
}
