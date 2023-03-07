namespace SillyWillyHomework.Entities
{
    public class Customer
    {
        public Customer() { }
        public Customer(string name)
        {
            Name = name;
        }
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
