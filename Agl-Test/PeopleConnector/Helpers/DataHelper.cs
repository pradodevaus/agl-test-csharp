using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Connectors.Helpers
{
    class DataHelper : IDataHelper
    {
        public string GetJsonData(string serviceUrl)
        {
            string data = string.Empty;

            using (WebClient wc = new WebClient())
            {
                data = wc.DownloadString(serviceUrl);
            }

            return data;
        }

        public T DeserializeData<T>(string jsonData)
        {
            var personList = JsonConvert.DeserializeObject<T>(jsonData);

            return personList;
        }
    }
}
