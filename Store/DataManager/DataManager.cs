using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DataManager
{
    public static class DataManager
    {
        /// <summary>
        /// Reads a JSON file from filepath, returns a JObject
        /// </summary>
        /// <param>String FilePath</param>
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
        /// <summary>
        /// Takes in an object and writes to file path
        /// </summary>
        /// <param>string FilePath, object db</param>
        static public JObject SerializeJSON(string FilePath, object db)
        {
            string json = JsonConvert.SerializeObject(db, Formatting.Indented);
            try
            {   // Open the text file using a stream reader.
                System.IO.File.WriteAllLines(@"../../data.buf", lines);
                File.Delete(@"../../data.json");
                System.IO.File.Move(@"../../data.buf", @"../../data.json");
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be Written:");
                Console.WriteLine(e.Message);
            }
                return null;
        }
    }
    //TODO: Open Write stream to buffer file to record changes 
}
