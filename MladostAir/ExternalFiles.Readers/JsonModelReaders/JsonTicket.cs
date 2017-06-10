using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class JsonTicket
    {
        [JsonProperty(PropertyName = "Destination")]
        public string Destination { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public int Price { get; set; }

        [JsonProperty(PropertyName = "Airline")]
        public string Airline { get; set; }

        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "Customer")]
        public JsonCustomer Customer { get; set; }

        [JsonProperty(PropertyName = "Airport")]
        public JsonAirport Airport { get; set; }
    }
}
