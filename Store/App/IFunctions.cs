using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    interface Insert<T>
    {
        DTO.Response Insert(T arg);
    }
    interface FindAll<T>
    {
        DTO.Response FindAll(string type, string searchvalue);
    }
    interface FindOne<T>
    {
        DTO.Response FindOne(string search);
    }
}
