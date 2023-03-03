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
        public Customer Customer { get; set; }
    }

}
