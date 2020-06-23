using System;
using System.Collections.Generic;

public static class Utils
{
    public static string ReflectActionToString(string type, string method) { 
    // Get a type from the string 
    Type t = Type.GetType(type);
    // Retrieve the method you are looking for
    MethodInfo Info = type.GetMethod(mymethod);
    // Invoke the method on the instance we created above
    return Info.Invoke(null, null);
    }
    
}
