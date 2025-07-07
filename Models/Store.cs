namespace API_Shop_Online.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
