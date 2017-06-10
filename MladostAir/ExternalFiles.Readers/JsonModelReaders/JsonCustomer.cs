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
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "CustomerNumber")]
        public int CustomerNumber { get; set; }

        [JsonProperty(PropertyName = "Age")]
        public int Age { get; set; }
    }
}
