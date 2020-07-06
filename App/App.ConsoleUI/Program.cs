using System;
using Newtonsoft.Json.Linq;
using App.API.Controllers;
using System.Collections.Generic;
using System.Linq;
//using Newtonsoft.Json;
namespace App.ConsoleUI
{
    class Program
    {
        private static Functions fn;
        public static void Parse(Functions fn)
        {
            var CustomerControls = new CustomerController();
            while (true)
            {
                Console.Write(">");
                try
                {
                    var input = fn.Parse(Console.ReadLine());
                    Console.WriteLine($"Found {input.Count} Results:");
                    if (input.Count > 0)
                    {
                        //List<string> keys = input[0].Properties().Select(p => p.Name).ToList();
                        //Console.WriteLine(String.Join("\t ", keys.ToArray()));
                        foreach (JObject j in input)
                        {
                            foreach (JProperty key in j.Properties())
                            {
                                Console.WriteLine(key.Name + ": " + key.Value);
                            }
                            Console.Write("\n");
                        };
                    }
                }
                catch (ArgumentException e)
                {
                    Console.Write(e.Message + "\n");
                    fn.Options();
                }
                catch (Exception e)
                {
                    Console.Write(e.GetType()+":");
                    Console.Write(e.Message+"\n");
                }


            }
        }
        static void Main(string[] args)
        {
            fn = new Functions();
            //API = new API.API();
            Console.WriteLine("Hello Welcome to the store!");
            Console.WriteLine("Here Are the available commands:");
            fn.Options();
            Parse(fn);
        }
    }
}
