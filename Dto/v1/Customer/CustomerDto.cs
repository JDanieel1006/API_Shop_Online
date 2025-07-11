﻿using System.ComponentModel.DataAnnotations;

namespace API_Shop_Online.Dto.v1.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
