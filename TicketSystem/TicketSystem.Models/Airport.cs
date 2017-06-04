using System.Collections.Generic;

namespace MladostAir.Models
{
    public class Airport
    {
        private ICollection<Airline> airlines;

        public int Id { get; set; }

        public string Name { get; set; }

        public string AirportCode { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Airline> Airlines
        {
            get { return this.airlines; }
            set { this.airlines = value; }
        }
    }
}
