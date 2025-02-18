using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pd311_mvc_aspnet.Models
{
    public class Product
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [MaxLength(255)]
        public string? Image { get; set; }


        [ForeignKey("Category")]
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
