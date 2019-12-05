using System.Security.Cryptography;
using System.Text;

namespace Moodis.Feature.Login
{
    public static class Crypto
    {
        public static string CalculateMD5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Dispose();
            return sb.ToString();
        }
    }
}
