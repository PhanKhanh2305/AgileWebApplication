using System.ComponentModel.DataAnnotations;

namespace AgileWebApplication.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Required]
        [StringLength(200)]
        public string? GenderName { get; set; }
    }
}
