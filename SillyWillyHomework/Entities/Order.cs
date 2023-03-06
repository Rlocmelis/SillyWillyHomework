using SillyWillyHomework.Models;

namespace SillyWillyHomework.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
