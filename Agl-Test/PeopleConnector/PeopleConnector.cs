using Agl.Common;
using Agl.Connectors.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Agl.Connectors
{
    public class PeopleConnector : IPeopleConnector
    {
        private readonly IDataHelper _dataHelper;
        private readonly ILogger _log = Log.ForContext<PeopleConnector>();

        public PeopleConnector()
        {
            _dataHelper = new DataHelper();
        }

        public PeopleConnector(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public List<Person> GetPeople()
        {
            var data = _dataHelper.GetJsonData(ConfigurationManager.AppSettings["PeopleServiceUrl"]);

            _log.Debug($"JSON data received from server: {data}");

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("Empty response returned from the server");
            }

            var personList = _dataHelper.DeserializeData<List<Person>>(data);

            return personList;
        }
    }
}