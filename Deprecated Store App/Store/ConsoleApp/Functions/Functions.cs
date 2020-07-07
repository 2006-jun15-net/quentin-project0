using System;
using System.Collections.Generic;
namespace ConsoleUI { 
public class Functions
{ 
    private List<Function> Options;
    private Dictionary<string, List<int>> Domains;
    private Dictionary<string, List<int>> Verbs;
    private Dictionary<string, Function> Actions;
    private string _prompt { get; set; }="Main Menu";
    public string Prompt => _prompt;
    public string Options()
    {
        var R = new List<string>();
        foreach(string k in Actions.Keys)
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
        return string.Join('\n', R.ToArray());
    }
    public DTO.Action Parse(string action)
    {
        Function fn;
        if (actions.TryGetValue(action, out fn))
        {
            DTO.action request = new DTO.Action();
            request.Endpoint = fn.Domain;
            request.Action = fn.Action;
            Console.WriteLine(fn.Verb + " " + fn.Domain + ":");
            Console.WriteLine("Building Query:");
            while (true)
            {
                Fields f = new Fields();
                foreach (string s in fn.QueryFields)
                {
                    Console.Write(s);
                    Console.Write(':');
                    var value = Console.ReadLine();
                    f[s] = value;
                }
                request.Payload.Add(f);

                if (fn.MultiQuery == true)
                {
                    Console.WriteLine("Add Another?: Y/N");
                    if (Console.ReadLine() == "N") break;
                }
                else break;
            }
            return request; 
        }
        else
        {
            throw new ArgumentException("Command Not Found", "argument");
        }

    }
    public Functions()
    {
        Options.Add(new Function()
        {
            Verb = "Add",
            Domain = "Customer",
            Info = "Add a Customer to the Database",
            Fields = new string[] {"Firstname", "LastName", "Location" },
            MultiQuery = true
        });
        Options.Add(new Function()
        {
            Verb = "Add",
            Domain = "Order",
            Info = "Place an order To a Location",
            QueryFields = new string[] {"Location", "Product name", "Qty"},
            MultiQuery = true
        });
        Options.Add(new Function()
        {
            Verb = "Show",
            Domain = "Order",
            Info = "Display Order Details",
            Model = DTO.Order,
            QueryFields = new string[] {"ID"}
        });
        Options.Add(new Function()
        {
            Verb = "Show",
            Domain = "Location",
            Info = "Show Location Order History",
            QueryFields = new string[] {"Name"}
        });
        Options.Add(new Function()
        {
            Verb = "Show",
            Domain = "Customer",
            Info = "Show A Customer Order History",
            QueryFields = new string[] {"ID", "FistName", "LastName" }
        });
        Options.Add(new Function()
        {
            Verb = "Back",
            Domain = "Utility",
            Info = "Up One Level in Menu",
            QueryFields = new string[] {}
        });
        Options.Add(new Function()
        {
            Verb = "Exit",
            Domain = "Utility",
            Info = "Leave the Store",
            QueryFields = new string[] {}
        });
        for (int i = 0; i<Options.Count; i++)
        {
            var o = Options[i];
            Domains[o.Domain].Add(i);
            Verbs[o.Verb].Add(i);
            Actions[o.Verb[0] + o.Domain[0]] = o;
        }
    }
}
}