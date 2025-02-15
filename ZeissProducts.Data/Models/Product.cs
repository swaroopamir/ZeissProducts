using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeissProducts.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR(200)")]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime ManufactureDate { get; set; }
        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime ExpiryDate { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
