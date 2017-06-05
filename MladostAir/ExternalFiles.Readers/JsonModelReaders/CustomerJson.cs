using Newtonsoft.Json;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class CustomerJson
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("customerNumber")]
        public int CustomerNumber { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
