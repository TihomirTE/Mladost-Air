using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class DataArray
    {
        [JsonProperty("data")]
        public IList<JsonTicket> Tickets { get; set; }
    }
}
