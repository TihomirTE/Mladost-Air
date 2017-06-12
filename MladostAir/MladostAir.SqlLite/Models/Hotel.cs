using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MladostAir.SqlLite.Models
{
    [Table("Hotels")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public int? StarRating { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }
    }
}
