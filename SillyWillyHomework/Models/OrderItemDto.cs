﻿using SillyWillyHomework.Entities;

namespace SillyWillyHomework.Models
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
