using DataManager;
using Newtonsoft.Json.Linq;
using DTO;
using System;

namespace StoreApp
{
    public class Store
    {
        //TO DO Make this take in a config file
        private void LoadData() 
        {
            JObject obj = DataManager.DataManager.DeserializeJSON("../data.json");
            DTO.DTO dto = obj.ToObject<DTO.DTO>();
        }
        public DTO.Response Action(DTO.Action action)
        {
            //To do catch if method does not exist and throw a usage error back of options
            var data = Utils.ReflectActionToString(action.endpoint, action.arguments);
            return data;
        }
    }
}
