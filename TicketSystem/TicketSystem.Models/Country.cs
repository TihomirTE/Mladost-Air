using System.Collections.Generic;

namespace MladostAir.Models
{
    public class Country
    {
        private ICollection<Airport> airports;

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Airport> Airports
        {
            get { return this.airports; }
            set { this.airports = value; }
        }
    }
}
