using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Moodis.Feature.Login
{
    public static class Serializer
    {
        public static bool Save(string filePath, object objToSerialize)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, objToSerialize);
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static T Load<T>(string filePath) where T : new()
        {
            T result = new T();

            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    result = (T)bin.Deserialize(stream);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }
    }

}
