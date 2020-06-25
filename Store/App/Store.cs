using DataManager;
using Newtonsoft.Json.Linq;
using DTO;
using System;

namespace StoreApp
{
    public class Store
    {
        private DBO DB;
        private Router router;
        //TO DO Make this take in a config file
        //<summary>
        //Reads json data and creates a new DBO object
        //</summary>
        //<param></param>
        private void LoadData() 
        {
            JObject obj = DataManager.DeserializeJSON("../data.json");
            DTO.DTO dto = obj.ToObject<DTO.DTO>();
            this.DB = DBO(dto);
        }
        //<summary>
        //Serializes DBO to json format
        //</summary>
        //<param></param>
        private void Save() 
        {
            DataManager.SerializeJSON("../data.json", this.DB);
        }
        //<summary>
        //The ways users interact with the store via DTO.action objects
        //</summary>
        //<param>DTO.Action action</param>
        public DTO.Response Route(DTO.Action action)
        {
            //To do catch if method does not exist and throw a usage error back of options
            Type t = Router[action.Endpoint];
            var r = t.GetMethod(action.action).Invoke(null, new object[]{action.payload});
        }
    }
}
