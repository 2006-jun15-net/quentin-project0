using System;
using System.Collections.Generic;

public  class Router
{
    public Type this[string key]
    {
    get{
            
            Type t = Type.GetType(key); 
        }
    }

}
