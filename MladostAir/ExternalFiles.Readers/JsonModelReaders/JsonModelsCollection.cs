using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalFiles.Readers.JsonModelReaders
{
    public class JsonModelsCollection
    {
        [JsonProperty("Ticket")]
        public DbSet<JsonTicket> Tickets { get; set; }

        //public IEnumerable<JsonAirport> Airports { get; set; }

        //public IEnumerable<JsonCustomer> Customers { get; set; }
    }
}
