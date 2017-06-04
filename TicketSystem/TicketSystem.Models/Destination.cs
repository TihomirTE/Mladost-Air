using System.Collections.Generic;

namespace MladostAir.Models
{
    public class Destination
    {
        private ICollection<Ticket> tickets;

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}