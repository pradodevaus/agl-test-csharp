using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Agl.Common
{
    [Serializable]
    public class Pet
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PetType Type { get; set; }
    }
}