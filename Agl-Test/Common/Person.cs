using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Common
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GenderType Gender { get; set; }

        public int Age { get; set; }

        public IList<Pet> Pets { get; set; }

        public Person()
        {
            this.Pets = new List<Pet>();
        }
    }
}
