using System;
using System.Collections.Generic;
namespace DataTransmissionObject
{
    public class Action
    {
        public List<Dictionary<string,string>> Payload;
        public string Endpoint;
        public string Function;
    }
    public class Response
    {
        public T Payload;
        public string message;
    }
}
