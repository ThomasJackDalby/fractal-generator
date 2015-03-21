using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Generator
{
    public class Serializer
    {
        public static void Save<T>(T result, string filename)
        {
            try
            {
                using (Stream stream = File.OpenWrite(filename))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static T Load<T>(string filename)
        {
            try
            {
                using (Stream stream = File.OpenRead(filename))
                {
                    IFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
        }
    }
}
