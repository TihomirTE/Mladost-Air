using MladostAir.Models.Enum;

namespace MladostAir.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int TicketNumber { get; set; }

        public TypeClass TypeClass { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int DestinationId { get; set; }

        public virtual Destination Destination { get; set; }

    }
}
