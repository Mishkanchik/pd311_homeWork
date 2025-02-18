using System.ComponentModel.DataAnnotations;

namespace pd311_mvc_aspnet.Models
{
    public class Category
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
