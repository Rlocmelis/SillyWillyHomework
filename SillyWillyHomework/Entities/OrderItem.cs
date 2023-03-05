namespace SillyWillyHomework.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
        // An Item should have a seperate price from the product, since the product price can change later
        public int UnitPrice { get; set; }
    }
}
