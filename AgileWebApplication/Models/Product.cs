using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AgileWebApplication.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(200)]
        public string? ProductName { get; set; }
        [StringLength(500)]
        public string? ProductDescription { get; set; }
        [Required]
        [StringLength(200)]
        public string? ProductPhoto { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,3)")]
        public decimal ProductPrice { get; set; }
        public bool BestSeller { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Category? Category { get; set; }
        public Gender? Gender { get; set; }
    }
}
