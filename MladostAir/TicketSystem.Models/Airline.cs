using MladostAir.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MladostAir.Models
{
    public class Airline
    {
        private ICollection<Customer> customers;

        private ICollection<Airport> airports;

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public TypeAircraft TypeAircraft { get; set; }

        public virtual ICollection<Customer> Customers
        {
            get { return this.customers; }
            set { this.customers = value; }
        }

        public virtual ICollection<Airport> Airports
        {
            get { return this.airports; }
            set { this.airports = value; }
        }
    }
}
