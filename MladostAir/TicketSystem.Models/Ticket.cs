using MladostAir.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace MladostAir.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public int Price { get; set; }

        public TypeClass TypeClass { get; set; }

        public TypeAircraft TypeAircraft { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int? AirportId { get; set; }

        public virtual Airport Airport { get; set; }

        public int? AirlineId { get; set; }

        public virtual Airline Airline { get; set; }
    }
}
