using FluentValidation;
using SillyWillyHomework.Entities;
using SillyWillyHomework.Validation;
using System;

namespace SillyWillyHomework.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
