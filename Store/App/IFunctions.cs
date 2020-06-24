using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    /// <summary>
    /// Insert Function: For interacting with our DB. 
    /// </summary>
    /// <param>Type</param>
    interface Insert<T>
    {
        DTO.Response Insert(T arg);
    }
    /// <summary>
    /// FindAll Function: For interacting with our DB. 
    /// </summary>
    /// <param>Type</param>
    interface FindAll<T>
    {
        DTO.Response FindAll(string type, string searchvalue);
    }
    /// <summary>
    /// FindOne Function: For interacting with our DB. 
    /// </summary>
    /// <param>Type</param>
    interface FindOne<T>
    {
        DTO.Response FindOne(string search);
    }
}
