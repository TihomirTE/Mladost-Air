using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class DataArray
    {
        [JsonProperty("data")]
        public List<TicketJson> Superheroes { get; set; }
    }
}
