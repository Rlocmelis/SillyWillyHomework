﻿using System.ComponentModel.DataAnnotations;

namespace SillyWillyHomework.Models.Requests
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
