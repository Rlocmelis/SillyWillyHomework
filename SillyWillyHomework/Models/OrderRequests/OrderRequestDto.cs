using SillyWillyHomework.Models.OrderRequests;

namespace SillyWillyHomework.Models
{
    public class OrderRequestDto
    {
        public int CustomerId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public List<OrderRequestItemDto> Items { get; set; }

    }
}
