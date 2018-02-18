using Agl.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Connectors
{
    public class PeopleConnector : IPeopleConnector
    {
        public List<Person> GetPeople()
        {
            var data = GetJsonData();

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("Empty response returned from the server");
            }

            var personList = DeserializeData(data);

            return personList;
        }

        private string GetJsonData()
        {
            string data = string.Empty;

            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadString("http://agl-developer-test.azurewebsites.net/people.json");
            }

            return data;
        }

        private List<Person> DeserializeData(string jsonData)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var personList = JsonConvert.DeserializeObject<List<Person>>(jsonData, settings);

            return personList;
        }
    }
}
