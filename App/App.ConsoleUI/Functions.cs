using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using App.DataAccess.Entities;
using App.API.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

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
            var ReturnObject = new List<JObject>();
            if (Actions.TryGetValue(action, out fn))
            {
                if (fn.QueryFields.Length > 0)
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
                            if (s.Contains("Id") )
                            {
                                int i = Int32.Parse(value);
                            }
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
                    if (fn.Verb == "Add")
                    {
                        if (fn.Domain == "Customer")
                        {
                            var controller = new CustomerController();
                            foreach(JObject j in QueryObjects)
                            {
                                Customer c = controller.Format(j);
                                var x = controller.Add(c);
                                ReturnObject.Add(JObject.FromObject(x));
                            }
                            
                            Console.WriteLine("Successfully Added Customer");
                        }
                        if (fn.Domain == "Order")
                        {
                            var controller = new OrderController();
                            int id=0;
                            foreach (JObject j in QueryObjects)
                            {
                                Order c = controller.Format(j);
                                c.Date = DateTime.Now;
                                if(c.OrderId==0 && id==0)
                                {
                                    id = controller.max()+1;
                                    c.OrderId = id;
                                }
                                if (c.OrderId == 0 && id != 0)
                                {
                                    c.OrderId = id;
                                }
                                controller.Add(c);
                            }
                            if (id != 0)
                            {
                                var j = new JObject();
                                j["New Order Id"] = id;
                                ReturnObject.Add(j);
                            }
                            
                        }
                    }
                    if (fn.Verb == "Show")
                    {
                        if (fn.Domain == "Customer")
                        {
                            var qobject = QueryObjects[0];
                            var controller = new OrderController();
                            var o = controller
                                .Where($"x=> x.CustomerId==\"{qobject["CustomerId"]}\"")
                                .Include(x => x.Location)
                                .Include(x => x.Product).ToList();
                            foreach(Order O in o)
                            {
                                var J = new JObject();
                                J["Location"] = O.Location.Name;
                                J["Product"] = O.Product.Name;
                                J["Qty"] = O.Qty;
                                J["Date"] = O.Date;
                                ReturnObject.Add(J);
                            }

                        }
                        if (fn.Domain == "Order")
                        {
                            var qobject = QueryObjects[0];
                            var controller = new OrderController();
                            List<Order> o = controller.Where($"OrderId=\"{qobject["Id"]}\"")
                                .Include(x=>x.Customer)
                                .Include(x=>x.Product)
                                .Include(x=>x.Location)
                                .ToList();
                            foreach (Order O in o)
                            {
                                var J = new JObject();
                                J["Location"] = O.Location.Name;
                                J["Product"] = O.Product.Name;
                                J["Customer"] = O.Customer.FirstName + " " + O.Customer.LastName;
                                J["Qty"] = O.Qty;
                                J["Date"] = O.Date;
                                ReturnObject.Add(J);
                            }
                        }
                        if (fn.Domain == "Location")
                        {
                            var qobject = QueryObjects[0];
                            var controller = new OrderController();
                            List<Order> o = controller.Where($"x=>x.LocationId=\"{qobject["Id"]}\"")
                                .Include(x=>x.Location)
                                .Include(x=>x.Product)
                                .Include(x=>x.Customer).ToList()
                                ;
                            foreach (Order O in o)
                            {
                                var J = new JObject();
                                J["Location"] = O.Location.Name;
                                J["Product"] = O.Product.Name;
                                J["Customer"] = O.Customer.FirstName + " " + O.Customer.LastName;
                                J["Qty"] = O.Qty;
                                J["Date"] = O.Date;
                                ReturnObject.Add(J);
                            }
                        }
                    }
                    if (fn.Verb == "Search")
                    {
                        if (fn.Domain == "Customer")
                        {
                            var qobject = QueryObjects[0];
                            var controller = new CustomerController();

                            string condition = $"FirstName=\"{qobject["FirstName"]}\" && LastName=\"{qobject["LastName"]}\"";
                            var found = controller.Where(condition);
                            foreach (Customer C in found)
                            {
                                var J = JObject.FromObject(C);
                                ReturnObject.Add(J);
                            }
                        }/*
                        if (fn.Domain == "Order")
                        {
                            var qobject = QueryObjects[0];
                            var controller = new OrderController();
                            var found = controller.Where($"x.Customer.FirstName=={qobject["FirstName"]} && x.Customer.LastName=={qobject["LastName"]}");
                            foreach (Customer C in found)
                            {
                                var J = JObject.FromObject(C);
                                ReturnObject.Add(J);
                            }
                        }
                        */
                    }
                }
                return ReturnObject;
            }
            
            else
            {
                throw new ArgumentException($"Command Not Found Please Try Again");
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
                QueryFields = new string[] { "Firstname", "LastName", "DefaultLocationId" },
                MultiQuery = true,
                Conversion = "Order"
            });
            this.Selections.Add(new Function()
            {
                Verb = "Add",
                Domain = "Order",
                Info = "Place an order To a Location",
                QueryFields = new string[] { "LocationId", "ProductId", "Qty", "CustomerId" },
                MultiQuery = true,
                Conversion = "Order"
            });
           this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Order",
                Info = "Display Order Details",
                QueryFields = new string[] { "Id" },
                Conversion = "Order"
           });
            this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Location",
                Info = "Show a Location's Order History",
                QueryFields = new string[] { "Id" },
                Conversion = "Order"
            });
            this.Selections.Add(new Function()
            {
                Verb = "Show",
                Domain = "Customer",
                Info = "Show A Customer's Order History",
                QueryFields = new string[] { "CustomerId" },
                Conversion = "Customer"
            });
            this.Selections.Add(new Function()
            {
                Verb = "Search",
                Domain = "Customer",
                Info = "Search for a Customer by name",
                QueryFields = new string[] { "FirstName", "LastName" },
                Conversion = "Customer"
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