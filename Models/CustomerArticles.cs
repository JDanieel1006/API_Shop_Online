﻿using API_Shop_Online.Common.Enum;

namespace API_Shop_Online.Models
{
    public class CustomerArticle
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public CustomerArticleStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
