namespace SillyWillyHomework.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
    }
}
