using System;
using System.Collections.Generic;

public  class Router
{
    private Dictionary<string, Type> Routes;
    public Router(){
    }
    public Type this[string key]
    {
    get{
            
            Type t = Type.GetType(key); 
        }
    }
    public void Main(){
		var r = new Router();
		

		Console.WriteLine(s.ToString());
	}
}
