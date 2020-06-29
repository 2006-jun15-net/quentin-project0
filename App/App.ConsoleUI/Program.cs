using System;
//using App.DataAccess.Entities;
//using Newtonsoft.Json;
namespace App.ConsoleUI
{
    class Program
    {
        private Functions fn;
        static void Main(string[] args)
        {
            var fn = new Functions();
            Console.WriteLine("Hello Welcome to the store!");
            fn.Options();

        }

    }
}
