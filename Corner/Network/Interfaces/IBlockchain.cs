using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corner.Network.Interfaces
{
    public interface IBlockchain<TData>  
    {
        void AddBlock(Block<TData> block);
        Block<TData> BuildBlock(List<TData> data);
        void AcceptBlock(Block<TData> typedBlock);
    }
}
