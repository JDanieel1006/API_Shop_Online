using System.ComponentModel.DataAnnotations;

namespace API_Shop_Online.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<StoreArticle>? StoreArticle { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
