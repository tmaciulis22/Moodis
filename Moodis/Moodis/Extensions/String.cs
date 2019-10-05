using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Extensions
{
    public static class String
    {
        public static string ValidateJson(this string json)
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
                return null;
            }
            else
            {
                return json;
            }
        }
    }
}
