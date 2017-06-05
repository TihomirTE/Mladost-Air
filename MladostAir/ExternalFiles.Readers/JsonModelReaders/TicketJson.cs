using Newtonsoft.Json;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class TicketJson
    {
        [JsonProperty("ticketNumber")]
        public int TicketNumber { get; set; }

        [JsonProperty("typeClass")]
        public string TypeClass { get; set; }

        [JsonProperty("typeAircraft")]
        public string TypeAircraft { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("airline")]
        public string Airline { get; set; }

        [JsonProperty("airport")]
        public string Airport { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
