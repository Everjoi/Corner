using Corner.Network.Cryptography.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Corner.Network
{
    public static class Helper
    {

        public static string CalculateHash(this IHashable hashable,params object[] data)
        {
            return CalculateHash(data);
        }

        public static string CalculateHash(params object[] data)
        {
            var _data = Serialize(data);
            using SHA256 sha256 = SHA256.Create();
            var buffer = Encoding.UTF8.GetBytes(_data);
            var hash = sha256.ComputeHash(buffer);
            return BitConverter.ToString(hash).Replace("-","").ToLower();
        }

        public static string Serialize(object data)
        {
            return JsonSerializer.Serialize(data);
        }

    }
}
