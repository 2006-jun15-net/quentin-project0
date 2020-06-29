using System;
using Newtonsoft.Json.Linq;
//using App.DataAccess.Entities;
//using Newtonsoft.Json;
namespace App.ConsoleUI
{
    class Program
    {
        private Functions fn;
        public static void Parse(Functions fn)
        {
            while (true)
            {
                Console.Write(fn.Prompt + ">");
                var input = fn.Parse(Console.ReadLine());
                foreach (JObject j in input)
                {
                    Console.WriteLine(j.ToString());
                }
            }
        }
        static void Main(string[] args)
        {
            var fn = new Functions();
            Console.WriteLine("Hello Welcome to the store!");
            Console.WriteLine("Here Are the available commands:");
            fn.Options();
            Parse(fn);
        }
    }
}
