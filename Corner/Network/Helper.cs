using Corner.Network.Cryptography.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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
