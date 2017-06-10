using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class JsonAirport
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "AirportCode")]
        public string AirportCode { get; set; }
    }
}
