using System.ComponentModel.DataAnnotations;

namespace API_Shop_Online.Models
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public string? Addres { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
