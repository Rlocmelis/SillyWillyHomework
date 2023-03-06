using SillyWillyHomework.Models.Requests;

namespace SillyWillyHomework.Models
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public List<OrderItemRequest> Items { get; set; }

    }
}
