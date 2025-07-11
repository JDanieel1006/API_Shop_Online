using System.ComponentModel.DataAnnotations;

namespace API_Shop_Online.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
