using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MladostAir.SqlLite.Models
{
    [Table("Cities")]
    public class City
    {
        private ICollection<Hotel> hotels;

        public City()
        {
            this.hotels = new HashSet<Hotel>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Hotel> Hotels
        {
            get { return this.hotels; }
            set { this.hotels = value; }
        }
    }
}
