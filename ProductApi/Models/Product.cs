﻿namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }

    }
}
