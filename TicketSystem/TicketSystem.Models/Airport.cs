using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MladostAir.Models
{
    public class Airport
    {
        private ICollection<Airline> airlines;

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
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
