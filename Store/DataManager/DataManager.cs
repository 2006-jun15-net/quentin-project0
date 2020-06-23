using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DataManager
{
    public class DataManager
    {
        static public JObject DeserializeJSON(string FilePath)
        {
            try
            {   // Open the text file using a stream reader.
                return JObject.Parse(File.ReadAllText(@"../data.json"));
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
                return null;
        }
    }
    //TODO: Open Write stream to buffer file to record changes 
}
