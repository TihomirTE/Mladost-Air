using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MladostAir.Models
{
    public class Customer
    {
        private ICollection<Ticket> tickets;
        private ICollection<Airline> airlines;

        public Customer()
        {
            this.tickets = new HashSet<Ticket>();
            this.airlines = new HashSet<Airline>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int CustomerNumber { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }

        public virtual ICollection<Airline> Airlines
        {
            get { return this.airlines; }
            set { this.airlines = value; }
        }

    }
}
