using System;

namespace Moodis.Extensions
{
    public static class StringExtension
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
