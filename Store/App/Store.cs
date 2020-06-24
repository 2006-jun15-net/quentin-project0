using DataManager;
using Newtonsoft.Json.Linq;
using DTO;
using System;

namespace StoreApp
{
    public class Store
    {
        private DBO DB;
        //TO DO Make this take in a config file
        //<summary>
        //Reads json data and creates a new DBO object
        //</summary>
        //<param></param>
        private void LoadData() 
        {
            JObject obj = DataManager.DataManager.DeserializeJSON("../data.json");
            DTO.DTO dto = obj.ToObject<DTO.DTO>();
            this.DB = DBO(dto);
        }
        //<summary>
        //Serializes DBO to json format
        //</summary>
        //<param></param>
        private void Save() 
        {
            DataManager.DataManager.SerializeJSON("../data.json", this.DB);
        }
        //<summary>
        //The ways users interact with the store via DTO.action objects
        //</summary>
        //<param></param>
        public DTO.Response Action(DTO.Action action)
        {
            //To do catch if method does not exist and throw a usage error back of options
            var data = Utils.ReflectActionToString(action.endpoint, action.arguments);
            return data;
        }
    }
}
