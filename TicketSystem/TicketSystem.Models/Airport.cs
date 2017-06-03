﻿namespace MladostAir.Models
{
    public class Airport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AirportCode { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
