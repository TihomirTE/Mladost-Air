using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MladostAir.Models
{
    public class City
    {
        private ICollection<Airport> airports;

        public City()
        {
            this.airports = new HashSet<Airport>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Airport> Airports
        {
            get { return this.airports; }
            set { this.airports = value; }
        }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}