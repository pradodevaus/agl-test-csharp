using System.Collections.Generic;
using Agl.Common;

namespace Agl.Connectors
{
    public interface IPeopleConnector
    {
        List<Person> GetPeople();
    }
}