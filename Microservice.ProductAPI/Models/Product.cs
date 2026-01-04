using Microservice.ProductAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.ProductAPI.Models
{
    [Table("Product")]
    public class Product :BaseEntity
    {

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Column("description")]
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("category_name")]
        [Required]
        [StringLength(50)]
        public string Category_Name { get; set; }

        [Column("image_url")]
        [Required]
        [StringLength(300)]
        public string Image_url { get; set; }
    }
}
