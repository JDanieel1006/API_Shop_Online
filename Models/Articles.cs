﻿namespace API_Shop_Online.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public ICollection<StoreArticle>? StoreArticle { get; set; }
        public ICollection<CustomerArticle>? CustomerArticle { get; set; }
        public ICollection<SaleArticle> SaleArticles { get; set; }
    }
}
