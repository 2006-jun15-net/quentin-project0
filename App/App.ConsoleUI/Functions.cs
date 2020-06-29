using System;
using System.Collections.Generic;
using Newtonsoft.json.Linq;
namespace App.ConsoleUI
{
    public class Functions
    {
        private List<Function> Selections;
        private Dictionary<string, List<int>> Domains;
        private Dictionary<string, List<int>> Verbs;
        private Dictionary<string, Function> Actions;
        private string _prompt { get; set; } = "Main Menu";
        public string Prompt => _prompt;
        public void Options()
        {
            var R = new List<string>();
            foreach (string k in Actions.Keys)
            {
                string r = k;
                r += '\t';
                r += Actions[k].Verb;
                r += ' ';
                r += Actions[k].Domain;
                r += '-';
                r += Actions[k].Info;
                R.Add(r);
            }
            Console.Write(string.Join('\n', R.ToArray()));
        }
        public List<Jobject> Parse(string action)
        {
            Function fn;
            if (Actions.TryGetValue(action, out fn))
            {
                Console.WriteLine(fn.Verb + " " + fn.Domain + ":");
                Console.WriteLine("Building Query:");
                while (true)
                {
                    //Create a new Jobject and assign query fields
                    foreach (string s in fn.QueryFields)
                    {
                        var Jobject = new JObject();
                        Console.Write(s);
                        Console.Write(':');
                        var value = Console.ReadLine();
                        Jobject[s] = value;
                    }
                    if (fn.MultiQuery == true)
                    {
                        Console.WriteLine("Add Another?: Y/N");
                        if (Console.ReadLine() == "N") break;
                    }
                    else break;
                }
                //Return a JOBJect list to calling function
                
            }
            else
            {
                throw new ArgumentException("Command Not Found", "argument");
            }

        }
        public Functions()
        {
            Selections.Add(new Function()
            {
                Verb = "Add",
                Domain = "Customer",
                Info = "Add a Customer to the Database",
                QueryFields = new string[] { "Firstname", "LastName", "Location" },
                MultiQuery = true
            });
            Selections.Add(new Function()
            {
                Verb = "Add",
                Domain = "Order",
                Info = "Place an order To a Location",
                QueryFields = new string[] { "Location", "Product name", "Qty" },
                MultiQuery = true
            });
            Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Order",
                Info = "Display Order Details",
                QueryFields = new string[] { "Id" }
            });
            Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Location",
                Info = "Show Location Order History",
                QueryFields = new string[] { "Name" }
            });
            Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Customer",
                Info = "Show A Customer Order History",
                QueryFields = new string[] { "FistName", "LastName" }
            });
            Selections.Add(new Function()
            {
                Verb = "Find",
                Domain = "Customer",
                Info = "Search for a Customer by name",
                QueryFields = new string[] { "FistName", "LastName" }
            });
            Selections.Add(new Function()
            {
                Verb = "Back",
                Domain = "Utility",
                Info = "Up One Level in Menu",
                QueryFields = new string[] { }
            });
            Selections.Add(new Function()
            {
                Verb = "Exit",
                Domain = "Utility",
                Info = "Leave the Store",
                QueryFields = new string[] { }
            });
            for (int i = 0; i < Selections.Count; i++)
            {
                var o = Selections[i];
                Domains[o.Domain].Add(i);
                Verbs[o.Verb].Add(i);
                Actions[Selections[i].Verb + Selections[i].Domain] = o;
            }
        }
    } 
    }