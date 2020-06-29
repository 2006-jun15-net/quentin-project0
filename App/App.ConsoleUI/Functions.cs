using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace App.ConsoleUI
{
    public class Functions
    {
        private List<Function> Selections = new List<Function>();
        //private Dictionary<string, List<int>> Domains;
        //private Dictionary<string, List<int>> Verbs;
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
            Console.WriteLine(string.Join('\n', R.ToArray()));
        }
        public List<JObject> Parse(string action)
        {
            Function fn;
            if (Actions.TryGetValue(action, out fn))
            {
                if (fn.Domain != null)
                {
                    Console.Write(fn.Verb + " " + fn.Domain + ":");
                    this._prompt = action;
                    Console.WriteLine("Query Builder:");
                    var QueryObjects = new List<JObject>();
                    while (true)
                    {
                        //Create a new Jobject and assign query fields
                        var Jobject = new JObject();
                        foreach (string s in fn.QueryFields)
                        {
                            Console.Write(s);
                            Console.Write(':');
                            var value = Console.ReadLine();
                            Jobject[s] = value;
                        }
                        QueryObjects.Add(Jobject);
                        if (fn.MultiQuery == true)
                        {
                            Console.WriteLine("Add Another?: Y/N");
                            if (Console.ReadLine() == "N") break;
                        }
                        else break;
                    }
                    //Return a JOBJect list to calling function
                    return QueryObjects;
                }
                else if (fn.Domain == "")
                {

                }
            }
            else
            {
                throw new ArgumentException("Command Not Found", "argument");
            }

        }
        public Functions()
        {
            //Domains = new Dictionary<string, List<int>>();
            //Verbs = new Dictionary<string, List<int>>();
            Actions = new Dictionary<string, Function>();
            this.Selections.Add(new Function()
            {
                Verb = "Add",
                Domain = "Customer",
                Info = "Add a Customer to the Database",
                QueryFields = new string[] { "Firstname", "LastName", "Location" },
                MultiQuery = true
            });
            this.Selections.Add(new Function()
            {
                Verb = "Add",
                Domain = "Order",
                Info = "Place an order To a Location",
                QueryFields = new string[] { "Location", "Product name", "Qty" },
                MultiQuery = true
            });
           this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Order",
                Info = "Display Order Details",
                QueryFields = new string[] { "Id" }
            });
            this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Location",
                Info = "Show Location Order History",
                QueryFields = new string[] { "Name" }
            });
            this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Customer",
                Info = "Show A Customer Order History",
                QueryFields = new string[] { "FistName", "LastName" }
            });
            this.Selections.Add(new Function()
            {
                Verb = "Find",
                Domain = "Customer",
                Info = "Search for a Customer by name",
                QueryFields = new string[] { "FistName", "LastName" }
            });
            this.Selections.Add(new Function()
            {
                Verb = "Back",
                Domain = "",
                Info = "Up One Level in Menu",
                QueryFields = new string[] { }
            });
            this.Selections.Add(new Function()
            {
                Verb = "Exit",
                Domain = "",
                Info = "Leave the Store",
                QueryFields = new string[] { }
            });
            for (int i = 0; i < Selections.Count; i++)
            {
                var o = Selections[i];
                //this.Domains[o.Domain].Add(i);
                //this.Verbs[o.Verb].Add(i);
                this.Actions[Selections[i].Verb + Selections[i].Domain] = o;
            }
        }
    } 
    }