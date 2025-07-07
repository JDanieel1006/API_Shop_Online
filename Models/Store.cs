namespace API_Shop_Online.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }

        public ICollection<StoreArticle> StoreArticle { get; set; }
    }
}
