using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class JsonCustomer
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
