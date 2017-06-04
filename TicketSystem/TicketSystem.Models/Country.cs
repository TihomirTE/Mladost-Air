using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MladostAir.Models
{
    public class Country
    {
        private ICollection<Airport> airports;

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Airport> Airports
        {
            get { return this.airports; }
            set { this.airports = value; }
        }
    }
}
