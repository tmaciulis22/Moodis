using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Extensions
{
    public static class String
    {
        public static string FromJsonToString(this string json)
        {
            try
            {
                json = json.Replace("[", " ").Replace("]", "").Replace(" ", "");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }

            if (string.IsNullOrEmpty(json))
            {
                return "";
            }
            else
            {
                return json;
            }
        }
    }
}
