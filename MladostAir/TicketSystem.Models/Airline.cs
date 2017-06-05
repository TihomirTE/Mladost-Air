using MladostAir.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MladostAir.Models
{
    public class Airline
    {
        private ICollection<Customer> customers;
        private ICollection<Ticket> tickets;

        public Airline()
        {
            this.customers = new HashSet<Customer>();
            this.tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers
        {
            get { return this.customers; }
            set { this.customers = value; }
        }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}
